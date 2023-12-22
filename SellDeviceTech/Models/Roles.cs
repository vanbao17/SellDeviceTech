using System;
using System.Collections.Generic;

namespace SellDeviceTech.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Customers = new HashSet<Customers>();
            GoogleProfile = new HashSet<GoogleProfile>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Customers> Customers { get; set; }
        public ICollection<GoogleProfile> GoogleProfile { get; set; }
    }
}
