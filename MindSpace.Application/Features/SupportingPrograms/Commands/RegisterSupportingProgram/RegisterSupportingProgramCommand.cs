using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram
{
    public class RegisterSupportingProgramCommand : IRequest
    {
        public int StudentId { get; set; }
        public int SupportingProgramId { get; set; }
    }
}
