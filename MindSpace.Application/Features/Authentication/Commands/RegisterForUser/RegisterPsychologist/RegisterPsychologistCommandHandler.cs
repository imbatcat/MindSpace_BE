using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterPsychologist.Specifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services.Authentication;
using MindSpace.Domain.Services.Authentication;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterPsychologist
{
    public class RegisterPsychologistCommandHandler(
        ILogger<RegisterPsychologistCommandHandler> logger,
        IApplicationUserService applicationUserService,
        IExcelReaderService excelReaderService,
        IUnitOfWork unitOfWork) : IRequestHandler<RegisterPsychologistCommand>
    {
        public async Task Handle(RegisterPsychologistCommand request, CancellationToken cancellationToken)
        {
            var results = await excelReaderService.ReadExcelAsync(request.file);

            foreach (var result in results)
            {
                Psychologist newPsychologist = new Psychologist()
                {
                    Email = result["Email"],
                    UserName = result["Username"],
                };

                var specializationSpecification = new SpecializationSpecifications(result["Specialization"]);
                var specialization = (await unitOfWork.Repository<Specialization>().GetAllWithSpecAsync(specializationSpecification)).FirstOrDefault();

                if (specialization == null)
                {
                    //insert new
                    specialization = new Specialization()
                    {
                        Name = result["Specialization"]
                    };
                    specialization = unitOfWork.Repository<Specialization>().Insert(specialization);
                    await unitOfWork.CompleteAsync();
                }
                newPsychologist.SpecializationId = specialization.Id;
                try
                {
                    await applicationUserService.InsertAsync(newPsychologist, result["Password"]);
                }
                catch (DuplicateUserException ex)
                {
                    logger.LogError(ex, "Duplicate user detected: {Email}", newPsychologist.Email);
                    // Handle duplicate user scenario
                }
                catch (CreateUserFailedException ex)
                {
                    logger.LogError(ex, "Failed to create user: {Email}", newPsychologist.Email);
                    // Handle user creation failure
                }
            }
        }
    }
}