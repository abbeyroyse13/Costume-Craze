﻿using CostumeCraze.Auth.Models;
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

    }
}