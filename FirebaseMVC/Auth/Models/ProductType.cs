using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostumeCraze.Auth.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal static void Add(ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
