using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? View { get; set; }
        public int? Like { get; set; }
        public int? Share { get; set; }
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }
        public int? CommentId { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
    }
}
