using System;
using System.Collections.Generic;

namespace SellDeviceTech.Models
{
    public partial class Products
    {
        public Products()
        {
            Cart = new HashSet<Cart>();
        }

        public int IdProduct { get; set; }
        public string TitleProduct { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? IdCate { get; set; }
        public string Image { get; set; }

        public Cates IdCateNavigation { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
