using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWGBRestaurantAutomation.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int OrderNumber { get; set; }
        public int OrderQuantity { get; set; }
        public int TableNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}