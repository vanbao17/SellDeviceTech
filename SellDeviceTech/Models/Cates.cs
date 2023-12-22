using System;
using System.Collections.Generic;

namespace SellDeviceTech.Models
{
    public partial class Cates
    {
        public Cates()
        {
            Products = new HashSet<Products>();
        }

        public int IdCate { get; set; }
        public string Title { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
