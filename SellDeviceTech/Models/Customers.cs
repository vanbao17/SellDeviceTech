using System;
using System.Collections.Generic;

namespace SellDeviceTech.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Cart = new HashSet<Cart>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }

        public Roles Role { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
