using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Services.AuthenticationServices;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Owned;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services.Authentication;
using MindSpace.Domain.Services.Authentication;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterManager
{
    public class RegisterSchoolManagerCommandHandler
        (ILogger<RegisterSchoolManagerCommandHandler> logger,
        IApplicationUserService applicationUserService,
        IExcelReaderService excelReaderService,
        IUnitOfWork unitOfWork) : IRequestHandler<RegisterSchoolManagerCommand>
    {
        public async Task Handle(RegisterSchoolManagerCommand request, CancellationToken cancellationToken)
        {
            var results = await excelReaderService.ReadExcelAsync(request.file);

            foreach (var result in results)
            {
                SchoolManager newSchoolManager = new SchoolManager()
                {
                    Email = result["Email"],
                    UserName = result["Username"],
                };

                Address newSchoolAddress = new Address()
                {
                    Street = result["AddressStreet"],
                    City = result["AddressCity"],
                    Ward = result["AddressWard"],
                    PostalCode = result["AddressPostalCode"],
                    Province = result["AddressProvince"]
                };

                School newSchool = new School()
                {
                    SchoolName = result["SchoolName"],
                    ContactEmail = result["ContactEmail"],
                    PhoneNumber = result["PhoneNumber"],
                    Address = newSchoolAddress,
                    Description = result["Description"],
                    CreateAt = DateTime.Now,
                    JoinDate = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                var school = unitOfWork.Repository<School>().Insert(newSchool);
                await unitOfWork.CompleteAsync();
                newSchoolManager.SchoolId = school.Id;
                try
                {
                    await applicationUserService.InsertAsync(newSchoolManager, result["Password"]);
                }
                catch (DuplicateUserException ex)
                {
                    logger.LogError(ex, "Duplicate user detected: {Email}", newSchoolManager.Email);
                    // Handle duplicate user scenario
                }
                catch (CreateUserFailedException ex)
                {
                    logger.LogError(ex, "Failed to create user: {Email}", newSchoolManager.Email);
                    // Handle user creation failure
                }
                school.SchoolManagerId = newSchoolManager.Id;
                unitOfWork.Repository<School>().Update(school);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}