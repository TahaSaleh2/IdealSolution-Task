using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Models.ViewModels
{
    public class ListResultVM<TEntity> where TEntity : class
    {
        public List<TEntity> Items { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }
        public int PageNumber { get; set; }
    }
}
