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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> products = _productRepo.GetAllProducts();

            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product product = _productRepo.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                _productRepo.AddProduct(product);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _productRepo.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                _productRepo.UpdateProduct(product);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
