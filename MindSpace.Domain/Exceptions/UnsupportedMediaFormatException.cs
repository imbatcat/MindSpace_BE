using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Exceptions
{
    public class UnsupportedMediaFormatException : Exception
    {
        // ==================================
        // === Props & Fields
        // ==================================

        public string FileExtension { get; private set; }

        // ==================================
        // === Constructors
        // ==================================

        public UnsupportedMediaFormatException(string fileExtension)
            : base($"File format {fileExtension} is not supported.")
        {
            FileExtension = fileExtension;
        }
    }
}
