using MindSpace.Domain.Entities.Identity;
using System.ComponentModel;

namespace MindSpace.Application.DTOs.SupportingPrograms
{
    public class SupportingProgramResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PdffileUrl { get; set; }
        public int MaxQuantity { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public bool IsActive { get; set; }
        public int PsychologistId { get; set; }
        public string PsychologistName { get; set; }
        public string StartDateAt { get; set; }
    }
}