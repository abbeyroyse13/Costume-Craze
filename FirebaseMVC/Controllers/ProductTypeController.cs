﻿using CostumeCraze.Auth.Models;
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProductTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ProductTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductTypeController/Delete/5
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
