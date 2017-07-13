using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
