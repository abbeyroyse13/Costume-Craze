using CostumeCraze.Auth.Models;
using CostumeCraze.Models;
using CostumeCraze.Models.ViewModels;
using CostumeCraze.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CostumeCraze.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductTypeRepository _productTypeRepo;
        private readonly IUserProfileRepository _userProfileRepo;

        public ProductController(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IUserProfileRepository userProfileRepository)
        {
            _productRepo = productRepository;
            _productTypeRepo = productTypeRepository;
            _userProfileRepo = userProfileRepository;
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
            var viewModel = new ProductFormViewModel();
            viewModel.ProductTypes = _productTypeRepo.GetAllProductTypes();

            return View(viewModel);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel productFormViewModel)
        {
            try
            {
                string UserProfileId = GetCurrentUserProfileId();
                var currentUser = _userProfileRepo.GetByUserProfileId(UserProfileId);
                productFormViewModel.Product.UserProfileId = currentUser.Id;

                _productRepo.AddProduct(productFormViewModel.Product);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(productFormViewModel.Product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var editViewModel = new ProductFormViewModel();
            editViewModel.ProductTypes = _productTypeRepo.GetAllProductTypes();
            editViewModel.Product = _productRepo.GetProductById(id);

            if (editViewModel.Product == null)
            {
                return NotFound();
            }

            return View(editViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductFormViewModel editViewModel)
        {
            try
            {
                //editViewModel.Product = _productRepo.GetProductById(id);
                _productRepo.UpdateProduct(editViewModel.Product);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(editViewModel);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = _productRepo.GetProductById(id);

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                _productRepo.DeleteProduct(id);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(product);
            }
        }
    private int GetLoggedUserProfileId()
    {
        string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(id);
    }
    private string GetCurrentUserProfileId()
    {
        string userProfileId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userProfileId;
    }
  }
}
