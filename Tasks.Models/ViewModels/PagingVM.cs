using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.Models.ViewModels
{
    public class PagingVM
    {
        public int PageNumber { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Search { get; set; }
    }
}
