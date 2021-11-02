using CostumeCraze.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostumeCraze.Models
{
    public interface IProductTypeRepository
    {
        public List<ProductType> GetAllProductTypes();
        public ProductType GetProductTypeById(int id);
        public void AddProductType(ProductType productType);
        public void UpdateProductType(ProductType productType);
        public void DeleteProductType(int productTypeId);
    }
}