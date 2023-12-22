using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellDeviceTech.Models
{
    public class GoogleProfileLogin
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public Roles Role { get; set; }
    }
}