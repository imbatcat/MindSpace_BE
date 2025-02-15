using MindSpace.Domain.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.DTOs.Resources
{
    public class BlogResponseDTO
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool isActive { get; set; }
        public string SpecializationName { get; set; }
        public string SchoolManagerName { get; set; }
        public List<SectionResponseDTO> Sections { get; set; } = new();

    }
}
