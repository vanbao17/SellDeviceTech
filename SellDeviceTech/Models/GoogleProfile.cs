using System;
using System.Collections.Generic;

namespace SellDeviceTech.Models
{
    public partial class GoogleProfile
    {
        public GoogleProfile()
        {
            Cart = new HashSet<Cart>();
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }

        public Roles Role { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
