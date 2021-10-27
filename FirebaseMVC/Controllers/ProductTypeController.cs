using CostumeCraze.Auth.Models;
using CostumeCraze.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostumeCraze.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepository _productTypeRepo;

        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepo = productTypeRepository;
        }

        // GET: ProductTypeController
        public ActionResult Index()
        {
            List<ProductType> productTypes = _productTypeRepo.GetAllProductTypes();

            return View(productTypes);
        }

        // GET: ProductTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductType productType)
        {
            try
            {
                _productTypeRepo.AddProductType(productType);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(productType);
            }
        }

        // GET: ProductTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            ProductType productType = _productTypeRepo.GetProductTypeById(id);

            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductType productType)
        {
            try
            {
                _productTypeRepo.UpdateProductType(productType);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(productType);
            }
        }

        // GET: ProductTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            ProductType productType = _productTypeRepo.GetProductTypeById(id);

            return View(productType);
        }

        // POST: ProductTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductType productType)
        {
            try
            {
                _productTypeRepo.DeleteProductType(id);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(productType);
            }
        }
    }
}
