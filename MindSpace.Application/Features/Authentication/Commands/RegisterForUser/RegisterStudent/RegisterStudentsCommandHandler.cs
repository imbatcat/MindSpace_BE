using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.FileReaderServices;
using MindSpace.Application.Specifications.SchoolSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterStudent
{
    public class RegisterStudentsCommandHandler(
        ILogger<RegisterStudentsCommandHandler> logger,
        IApplicationUserRepository applicationUserService,
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
                    await applicationUserService.AssignRoleAsync(newStudent, UserRoles.Student);
                }
                catch (DuplicateUserException ex)
                {
                    logger.LogError(ex, "Duplicate user detected: {Email}", newStudent.Email);
                    // Handle duplicate user scenario
                }
                catch (CreateFailedException ex)
                {
                    logger.LogError(ex, "Failed to create user: {Email}", newStudent.Email);
                    // Handle user creation failure
                }
            }
        }
    }
}