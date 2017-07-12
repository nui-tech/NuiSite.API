using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class User
    {
        public User()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string FirebaseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public int? RoleId { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
