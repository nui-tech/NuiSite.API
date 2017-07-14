using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class TagMap
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
