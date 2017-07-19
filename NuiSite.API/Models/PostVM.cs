using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuiSite.API.Models
{
    public class PostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
