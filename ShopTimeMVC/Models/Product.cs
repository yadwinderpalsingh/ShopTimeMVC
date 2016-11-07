using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTimeMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public double Price { get; set; }
        public ProductType Gender { get; set; }

        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product { Id = 1, Name = "Cap", Avatar = "/men/1.jpg", Gender = ProductType.MEN, Price = 45.99 },
                new Product { Id = 2, Name = "Glasses", Avatar = "/men/2.jpg", Gender = ProductType.MEN, Price = 105.99 },
                new Product { Id = 3, Name = "Shoe", Avatar = "/men/3.jpg", Gender = ProductType.MEN, Price = 125.99 },
                new Product { Id = 4, Name = "Cap", Avatar = "/men/4.jpg", Gender = ProductType.MEN, Price = 25.99 },
                new Product { Id = 5, Name = "Dress 1", Avatar = "/women/1.jpg", Gender = ProductType.WOMEN, Price = 62.99 },
                new Product { Id = 6, Name = "Dress 2", Avatar = "/women/2.jpg", Gender = ProductType.WOMEN, Price = 89.99 },
                new Product { Id = 7, Name = "Dress 3", Avatar = "/women/3.jpg", Gender = ProductType.WOMEN, Price = 99.99 },
                new Product { Id = 8, Name = "Dress 1", Avatar = "/women/4.jpg", Gender = ProductType.WOMEN, Price = 45.99 },
                new Product { Id = 9, Name = "Shorts", Avatar = "/kids/1.jpg", Gender = ProductType.KIDS, Price = 55.99 },
                new Product { Id = 10, Name = "T-shirts", Avatar = "/kids/2.jpg", Gender = ProductType.KIDS, Price = 87.99 },
                new Product { Id = 11, Name = "T-shirt", Avatar = "/kids/3.jpg", Gender = ProductType.KIDS, Price = 56.99 },
                new Product { Id = 12, Name = "Jeans", Avatar = "/kids/4.jpg", Gender = ProductType.KIDS, Price = 87 },
            };
        }

        public List<Product> GetProductsByGender(string gender)
        {
            return GetProducts().Where(x => x.Gender.ToString().ToLower() == gender).ToList();
        }

        public Product GetProduct(int id)
        {
            return GetProducts().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}