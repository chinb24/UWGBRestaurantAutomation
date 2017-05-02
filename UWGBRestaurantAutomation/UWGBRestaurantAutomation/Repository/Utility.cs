using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UWGBRestaurantAutomation.DAL;

namespace UWGBRestaurantAutomation.Repository
{
    public static class Utility
    {
        // Gets user role by email
        public static string GetRole(string Email)
        {
            RestaurantContext db = new RestaurantContext();
            var Customers = db.Customers.Where(x => x.CustomerEmail == Email).First();
            string role = Customers.Role;
            return role;
        }
    }
}