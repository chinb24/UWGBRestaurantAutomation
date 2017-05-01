using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWGBRestaurantAutomation.Models
{
    public class CustomerViewModel
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public bool Ordering { get; set; }
    }
}