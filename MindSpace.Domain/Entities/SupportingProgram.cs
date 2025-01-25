using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities
{
    public class SupportingProgram : BaseEntity
    {
        // Fields
        public string ThumbnailUrl { get; set; }
        public string PdffileUrl { get; set; }
        public int MaxQuantity { get; set; }
        public Address Address { get; set; }
        public DateTime StartDateAt { get; set; }


        // 1 Manager - M SupportingProgram
        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }


        // 1 Psychologist - M SupportingProgram
        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }


        // 1 School - M Supporting Program
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}
