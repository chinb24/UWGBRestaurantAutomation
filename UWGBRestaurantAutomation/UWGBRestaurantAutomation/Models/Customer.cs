using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWGBRestaurantAutomation.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}