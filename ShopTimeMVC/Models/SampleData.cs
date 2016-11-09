using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopTimeMVC.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ShopTimeEntities>
    {
        protected override void Seed(ShopTimeEntities context)
        {
            new List<Product>
            {
                new Product { Id = 1, Name = "Cap", Avatar = "/men/1.jpg", Gender = ProductType.MEN, Price = 45.99M },
                new Product { Id = 2, Name = "Glasses", Avatar = "/men/2.jpg", Gender = ProductType.MEN, Price = 105.99M },
                new Product { Id = 3, Name = "Shoe", Avatar = "/men/3.jpg", Gender = ProductType.MEN, Price = 125.99M },
                new Product { Id = 4, Name = "Cap", Avatar = "/men/4.jpg", Gender = ProductType.MEN, Price = 25.99M },
                new Product { Id = 5, Name = "Dress 1", Avatar = "/women/1.jpg", Gender = ProductType.WOMEN, Price = 62.99M },
                new Product { Id = 6, Name = "Dress 2", Avatar = "/women/2.jpg", Gender = ProductType.WOMEN, Price = 89.99M },
                new Product { Id = 7, Name = "Dress 3", Avatar = "/women/3.jpg", Gender = ProductType.WOMEN, Price = 99.99M },
                new Product { Id = 8, Name = "Dress 1", Avatar = "/women/4.jpg", Gender = ProductType.WOMEN, Price = 45.99M },
                new Product { Id = 9, Name = "Shorts", Avatar = "/kids/1.jpg", Gender = ProductType.KIDS, Price = 55.99M },
                new Product { Id = 10, Name = "T-shirts", Avatar = "/kids/2.jpg", Gender = ProductType.KIDS, Price = 87.99M },
                new Product { Id = 11, Name = "T-shirt", Avatar = "/kids/3.jpg", Gender = ProductType.KIDS, Price = 56.99M },
                new Product { Id = 12, Name = "Jeans", Avatar = "/kids/4.jpg", Gender = ProductType.KIDS, Price = 87 }
            }.ForEach(x => context.Products.Add(x));
        }
    }
}