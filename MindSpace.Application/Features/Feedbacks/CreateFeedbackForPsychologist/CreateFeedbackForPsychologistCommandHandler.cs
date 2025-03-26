using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.FeedbackSpecifications;
using MindSpace.Application.Specifications.PsychologistsSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist
{
    public class CreateFeedbackForPsychologistCommandHandler(
        IUnitOfWork unitOfWork,
        IApplicationUserService<Psychologist> applicationUserService,
        ILogger<CreateFeedbackForPsychologistCommandHandler> logger,
        IMapper mapper
        ) : IRequestHandler<CreateFeedbackForPsychologistCommand>
    {
        public async Task Handle(CreateFeedbackForPsychologistCommand request, CancellationToken cancellationToken)
        {
            // Get psychologist, count number of rating
            var psychologist = await applicationUserService.GetUserWithSpec(new PsychologistSpecification(request.PsychologistId))
                ?? throw new NotFoundException($"Psycholigist with id {request.PsychologistId} not found");

            // List of feedbacks 
            var feedBacks = await unitOfWork.Repository<Feedback>().GetAllWithSpecAsync(new FeedbackSpecification(psychologist.Id));

            // Update psychologist's rating
            psychologist.AverageRating = (feedBacks.Count == 0)
                ? ((float)request.Rating)
                : ((psychologist.AverageRating * feedBacks.Count) + ((float)request.Rating)) / (feedBacks.Count + 1);
            await applicationUserService.UpdateAsync(psychologist);

            // Add new feedback to the table
            var feedbackToCreate = mapper.Map<Feedback>(request);
            var createdFeedback = unitOfWork.Repository<Feedback>().Insert(feedbackToCreate)
                ?? throw new CreateFailedException("Create feedback failed.");

            // Update into the database
            await unitOfWork.CompleteAsync();
        }
    }
}
