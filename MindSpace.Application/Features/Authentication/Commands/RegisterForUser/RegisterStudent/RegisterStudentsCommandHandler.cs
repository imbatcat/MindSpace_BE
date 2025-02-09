using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Specifications.SchoolSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Services.Authentication;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterStudent
{
    public class RegisterStudentsCommandHandler(
        ILogger<RegisterStudentsCommandHandler> logger,
        IApplicationUserService applicationUserService,
        IExcelReaderService excelReaderService,
        IUnitOfWork unitOfWork) : IRequestHandler<RegisterStudentsCommand>
    {
        public async Task Handle(RegisterStudentsCommand request, CancellationToken cancellationToken)
        {
            var results = await excelReaderService.ReadExcelAsync(request.file);

            foreach (var result in results)
            {
                Student newStudent = new Student()
                {
                    Email = result["Email"],
                    UserName = result["Username"],
                };

                var schoolSpecifications = new SchoolSpecifications(result["SchoolName"]);
                var school = (await unitOfWork.Repository<School>().GetAllWithSpecAsync(schoolSpecifications)).FirstOrDefault();

                if (school == null)
                {
                    logger.LogWarning("School {schoolName} does not exists", result["SchoolName"]);
                    //add to "status" object here
                    continue;
                }
                school.Students.Add(newStudent);
                try
                {
                    await applicationUserService.InsertAsync(newStudent, result["Password"]);
                }
                catch (DuplicateUserException ex)
                {
                    logger.LogError(ex, "Duplicate user detected: {Email}", newStudent.Email);
                    // Handle duplicate user scenario
                }
                catch (CreateUserFailedException ex)
                {
                    logger.LogError(ex, "Failed to create user: {Email}", newStudent.Email);
                    // Handle user creation failure
                }
            }
        }
    }
}