using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UWGBRestaurantAutomation.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderNumber { get; set; }
        public decimal Total { get; set; }
        public string CCName { get; set; }
        //[StringLength(16, ErrorMessage = "Credit Card Number Must Be 16 Digits", ErrorMessageResourceName = "CCNumber", ErrorMessageResourceType = typeof(String), MinimumLength = 16)]
        public string CCNumber { get; set; }
        //[StringLength(2, ErrorMessage = "Credit Card Number Must Be 16 Digits", ErrorMessageResourceName = "CCNumber", ErrorMessageResourceType = typeof(int), MinimumLength = 2)]
        //[MaxLength(2, ErrorMessage = "Month Must Be 2 Digits", ErrorMessageResourceName = "CCMonth", ErrorMessageResourceType = typeof(int))]
        //[MinLength(2, ErrorMessage = "Month Must Be 2 Digits", ErrorMessageResourceName = "CCMonth", ErrorMessageResourceType = typeof(int))]
        public int CCMonth { get; set; }
        //[StringLength(2, ErrorMessage = "Credit Card Number Must Be 16 Digits", ErrorMessageResourceName = "CCNumber", ErrorMessageResourceType = typeof(int), MinimumLength = 2)]
        public int CCYear { get; set; }
        //[StringLength(3, ErrorMessage = "Credit Card Number Must Be 16 Digits", ErrorMessageResourceName = "CCNumber", ErrorMessageResourceType = typeof(int), MinimumLength = 3)]
        public int CCSecurity { get; set; }
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}