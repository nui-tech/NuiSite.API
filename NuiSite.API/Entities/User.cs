using System;
using System.Collections.Generic;

namespace NuiSite.API.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirebaseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? LastInfoUpdated { get; set; }
        public string RoleId { get; set; }
    }
}
