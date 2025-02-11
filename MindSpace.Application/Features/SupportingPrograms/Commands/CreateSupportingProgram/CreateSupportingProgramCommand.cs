using MediatR;
using MindSpace.Application.DTOs.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands
{
    public class CreateSupportingProgramCommand : IRequest<SupportingProgramResponseDTO>
    {
        public string ThumbnailUrl { get; set; }
        public string PdffileUrl { get; set; }
        public int MaxQuantity { get; set; }
        public int SchoolManagerId { get; set; }
        public int PsychologistId { get; set; }
        public int SchoolId { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public DateTime StartDateAt { get; set; } = DateTime.Now;
    }
}
