using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UWGBRestaurantAutomation.Models;
using System.Data.Entity;

namespace UWGBRestaurantAutomation.DAL
{
    public class RestaurantInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RestaurantContext>
    {
        protected override void Seed(RestaurantContext context)
        {
            var customers = new List<Customer>
            {
                new Customer{FirstName="Carson",LastName="Alexander", CustomerEmail="customer@gmail.com"},
                new Customer{FirstName="Meredith",LastName="Alonso", CustomerEmail="customer2@gmail.com"},
                new Customer{FirstName="Arturo",LastName="Anand", CustomerEmail="customer3@gmail.com"},
                new Customer{FirstName="Gytis",LastName="Barzdukas", CustomerEmail="customer4@gmail.com"},
                new Customer{FirstName="Peggy",LastName="Justice", CustomerEmail="customer5@gmail.com"}
            };

            customers.ForEach(x => context.Customers.Add(x));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product {ProductName="Bone-in Pork* Chop with Honey Apple Chutney", ProductDescription="Cajun-seasoned hand-cut pork chop, apple chutney, maple butter.", ProductPrice=Convert.ToDecimal("13.99") },
                new Product {ProductName="Smokin' Double Steak* & Egg*", ProductDescription="Two hand-cut 4 oz. USDA Choice top sirloins, fried egg, stout gravy, bacon jam.", ProductPrice=Convert.ToDecimal("10.99") },
                new Product {ProductName="Cedar Salmon with Maple Mustard Glaze", ProductDescription="Cedar-seasoned salmon, maple mustard glaze, sautéed spinach.", ProductPrice=Convert.ToDecimal("14.49") },
                new Product {ProductName="Sirloin* Stir-Fry", ProductDescription="USDA Choice top sirloin, stir-fry veggies, dumpling sauce and wonton strips served with white rice.", ProductPrice=Convert.ToDecimal("12.99") },
                new Product {ProductName="Shrimp Wonton Stir Fry", ProductDescription="Stir-fry veggies, dumpling sauce, wonton strips, white rice.", ProductPrice=Convert.ToDecimal("10.99") },
                new Product {ProductName="4-Cheese Mac + Cheese with Honey Pepper Chicken Tenders", ProductDescription="Honey pepper balances the richness of this dish by giving a sweet and savory flavor to our breaded chicken tenders. ", ProductPrice=Convert.ToDecimal("12.49") },
                new Product {ProductName="Double Crunch Shrimp", ProductDescription="A generous portion of succulent shrimp golden fried to crunchy perfection is served with fries, coleslaw and cocktail sauce.", ProductPrice=Convert.ToDecimal("12.49") }
            };

            products.ForEach(x => context.Products.Add(x));
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order { CustomerId=1, ProductId=1, OrderNumber=3112, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=1 },
                new Order { CustomerId=1, ProductId=3, OrderNumber=3112, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=1 },
                new Order { CustomerId=2, ProductId=4, OrderNumber=3113, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=7 },
                new Order { CustomerId=2, ProductId=5, OrderNumber=3113, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=7 },
                new Order { CustomerId=3, ProductId=7, OrderNumber=3114, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=6 },
                new Order { CustomerId=3, ProductId=1, OrderNumber=3114, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=6 },
                new Order { CustomerId=3, ProductId=3, OrderNumber=3114, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=6 },
                new Order { CustomerId=4, ProductId=2, OrderNumber=3115, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=5 },
                new Order { CustomerId=4, ProductId=3, OrderNumber=3115, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=5 },
                new Order { CustomerId=4, ProductId=6, OrderNumber=3115, OrderQuantity=1, OrderDate=DateTime.Now, TableNumber=5 }
            };

            orders.ForEach(x => context.Orders.Add(x));
            context.SaveChanges();
        }
    }
}