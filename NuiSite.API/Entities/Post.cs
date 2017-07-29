using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class Post
    {
        public Post()
        {
            TagMap = new HashSet<TagMap>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TagMap> TagMap { get; set; }
    }
}
