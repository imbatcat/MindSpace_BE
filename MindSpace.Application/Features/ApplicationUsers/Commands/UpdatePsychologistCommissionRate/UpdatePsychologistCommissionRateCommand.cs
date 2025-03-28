using MediatR;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.UpdatePsychologistCommissionRate
{
    public class UpdatePsychologistCommissionRateCommand : IRequest
    {
        public int PsychologistId { get; set; }
        public decimal CommissionRate { get; set; }
    }
}