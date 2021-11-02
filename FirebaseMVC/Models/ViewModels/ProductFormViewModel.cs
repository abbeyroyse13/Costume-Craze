using CostumeCraze.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostumeCraze.Models;

namespace CostumeCraze.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; }
        public List<ProductType> ProductTypes { get; set; }
    }
}
