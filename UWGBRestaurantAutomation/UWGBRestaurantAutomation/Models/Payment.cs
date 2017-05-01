using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWGBRestaurantAutomation.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderNumber { get; set; }
        public decimal Total { get; set; }
        public string CCName { get; set; }
        public string CCNumber { get; set; }
        public int CCMonth { get; set; }
        public int CCYear { get; set; }
        public int CCSecurity { get; set; }
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}