using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications
{
    public class BasePagingParams
    {
        // Page Index
        public int PageIndex { get; set; } = 1;

        // Page Size
        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

    }
}
