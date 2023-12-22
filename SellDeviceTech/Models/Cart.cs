using System;
using System.Collections.Generic;

namespace SellDeviceTech.Models
{
    public partial class Cart
    {
        public int IdCart { get; set; }
        public int? IdProduct { get; set; }
        public int? Quality { get; set; }
        public int? CustomerId { get; set; }
        public string Id { get; set; }

        public Customers Customer { get; set; }
        public GoogleProfile IdNavigation { get; set; }
        public Products IdProductNavigation { get; set; }


        //public static implicit operator Cart(Cart v)
        //{
        //   throw new NotImplementedException();
        //}
    }
}
