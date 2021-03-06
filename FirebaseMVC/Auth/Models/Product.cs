using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostumeCraze.Models;

namespace CostumeCraze.Auth.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<ProductType> ProductTypes { get; set; }
    }
}
