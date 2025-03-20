using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.UnregisterSupportingProgram
{
    public class UnregisterSupportingProgramCommand : IRequest
    {
        public int StudentId { get; set; }
        public int SupportingProgramId { get; set; }
    }
}
