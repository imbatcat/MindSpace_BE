using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications.FeedbackSpecifications
{
    public class FeedbackSpecification : BaseSpecification<Feedback>
    {
        public FeedbackSpecification(int psychologistId) : base(x => x.PsychologistId == psychologistId)
        {
        }
    }
}
