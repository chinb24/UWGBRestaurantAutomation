using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWGBRestaurantAutomation.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}