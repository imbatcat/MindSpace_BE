using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Identity
{
    public class Manager : ApplicationUser
    {
        // 1 Psychologist - 1 User 
        public virtual ApplicationUser User { get; set; }

        // 1 Manager - M SupportingProgram
        public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();
    }
}