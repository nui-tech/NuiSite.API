using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            TagMap = new HashSet<TagMap>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TagMap> TagMap { get; set; }
    }
}
