using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GStoreApp.Library;
using GStore.WebUI.Models;
using NLog;

namespace GStore.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepo iRepo;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public CustomerController(IRepo repository)
        {
            iRepo = repository;
        }

        public ActionResult Index()
        {

            return View();
        }

        //GET
        public ActionResult CustomerInfo()
        {
            var customerInfo = new CustomerViewModel();

            return View(customerInfo);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerInfo( CustomerViewModel cvModel )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cvModel);
                }

                var format = new FormatHandler();

                Customer customer = new Customer
                {
                    CustomerId = cvModel.customerId,
                    FirstName = format.NameFormat(cvModel.FirstName),
                    LastName = format.NameFormat(cvModel.LastName),
                    PhoneNumber = format.PhoneCheck(cvModel.Phone),
                    FavoriteStore = cvModel.FavoriteStore
                };

                iRepo.AddCustomer(customer);
                logger.Info("CustomerController: New Customer is Added");

                return RedirectToAction(nameof(Index));
            }
            catch ( InvalidOperationException ex)
            {
                return View(cvModel);
            }
        }
    }
}