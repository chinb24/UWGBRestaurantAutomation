using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UWGBRestaurantAutomation.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace UWGBRestaurantAutomation.DAL
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("RestaurantContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<UWGBRestaurantAutomation.Models.Payment> Payments { get; set; }
    }
}