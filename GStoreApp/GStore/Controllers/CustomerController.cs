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

            return View();
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
                    int test = 0;
                    return View(cvModel);
                }

                var format = new FormatHandler();

                Customer customer = new Customer
                {
                    CustomerId = 0,
                    FirstName = format.NameFormat(cvModel.FirstName),
                    LastName = format.NameFormat(cvModel.LastName),
                    PhoneNumber = format.PhoneCheck(cvModel.Phone),
                    FavoriteStore = cvModel.FavoriteStore
                };

                if ( iRepo.SearchCustomer(customer).Count() > 0 )
                {
                    logger.Info("CustomerController: Existed Customer Find");
                    logger.Info(iRepo.SearchCustomer(customer).ToString());
                    ViewData["Customer"] = cvModel;
                    return View("PlaceOrder");
                }

                iRepo.AddCustomer(customer);
                logger.Info("CustomerController: New Customer is Added");
                ViewData["Customer"] = cvModel;
                return View("PlaceOrder");
            }
            catch ( InvalidOperationException ex )
            {
                return View(cvModel);

            }
        }

        public ActionResult PlaceOrder()
        {


            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder( OrderViewModel ovm )
        {
            

            return View();
        }
    }
}