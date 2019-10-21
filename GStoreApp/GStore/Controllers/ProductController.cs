using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GStoreApp.Library;
using NLog;
using GStore.WebUI.Models;

namespace GStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepo iRepo;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public ProductController(IRepo repository)
        {
            iRepo = repository;
        }

        /// <summary>
        /// GET, call repository to grab all product's data
        /// then pass it to view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable < Product > allProducts = iRepo.SearchProduct();

            var products = allProducts.Select(a => new ProductViewModel
            {
                ProductId = a.ProductId,
                ProductName = a.ProductName,
                UnitPrice = a.UnitPrice
            });
            return View(products);
        }
    }
}