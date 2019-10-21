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

        /// <summary>
        /// GET, default action for customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// GET, default action for customerInfo page
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerInfo()
        {
            var customerInfo = new CustomerViewModel();

            return View();
        }

        /// <summary>
        /// POST, receieve customer info in viewmodel from view,
        /// then call repository method to search/add customer with database
        /// then go into placing order view
        /// </summary>
        /// <param name="cvModel"></param>
        /// <returns></returns>
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
                    FirstName = format.NameFormat(cvModel.FirstName),
                    LastName = format.NameFormat(cvModel.LastName),
                    PhoneNumber = format.PhoneCheck(cvModel.Phone),
                    FavoriteStore = cvModel.FavoriteStore
                };

                if (iRepo.SearchCustomer(customer).Count() > 0)
                {
                    logger.Info("CustomerController: Existed Customer Find");
                    customer.CustomerId = iRepo.SearchCustomer(customer).ToList()[0].CustomerId;
                    ViewData["Customer"] = customer.CustomerId;
                    TempData["Customer"] = customer.CustomerId;
                    ViewData["Store"] = (int)customer.FavoriteStore;
                    TempData["Store"] = (int)customer.FavoriteStore;
                    List<Product> products = iRepo.SearchProduct().ToList();
                    List<decimal> unitPrice = new List<decimal>();
                    PriceViewModel price = new PriceViewModel
                    {
                        Price = unitPrice
                    };

                    for (int i = 0; i < products.Count; i++)
                    {
                        price.Price.Add(products[i].UnitPrice);
                    }
                    ViewData["Price"] = price;

                    return View("PlaceOrder");
                }
                else
                {
                    iRepo.AddCustomer(customer);
                    customer.CustomerId = iRepo.SearchCustomer(customer).ToList()[0].CustomerId;
                    logger.Info("CustomerController: New Customer is Added");
                    List<Product> products = iRepo.SearchProduct().ToList();
                    List<decimal> unitPrice = new List<decimal>();
                    PriceViewModel price = new PriceViewModel
                    {
                        Price = unitPrice
                    };

                    for (int i = 0; i < products.Count; i++)
                    {
                        price.Price.Add(products[i].UnitPrice);
                    }
                    ViewData["Price"] = price;
                    ViewData["Customer"] = customer.CustomerId;
                    TempData["Customer"] = customer.CustomerId;
                    ViewData["Store"] = (int)customer.FavoriteStore;
                    TempData["Store"] = (int)customer.FavoriteStore;

                    return View("PlaceOrder");
                }
            }
            catch ( InvalidOperationException ex )
            {
                return View(cvModel);

            }
        }
        /// <summary>
        /// GET, default view for place order
        /// </summary>
        /// <returns></returns>
        public ActionResult PlaceOrder()
        {
            var ovm = new OrderViewModel();

            return View();
        }

        /// <summary>
        /// POST, receieve data in view model from customer
        /// then call repository to check inventory in database
        /// if not enough, reject the order. Or update the inventory
        /// and add new order over view and order detail into database
        /// Finally, go to order complete page
        /// </summary>
        /// <param name="ovm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder( OrderViewModel ovm )
        {

            int customerId = (int)TempData["Customer"];
            int storeId = (int)TempData["Store"];
            List<Product> products = iRepo.SearchProduct().ToList();
            List<decimal> unitPrice = new List<decimal>();
            PriceViewModel price = new PriceViewModel
            {
                Price = unitPrice
            };

            for (int i = 0; i < products.Count; i++)
            {
                price.Price.Add(products[i].UnitPrice);
            }

            bool notAllZero = ovm.NSAmount == 0 && ovm.PS4PAmount == 0
                            && ovm.XBOAmount == 0 && ovm.PS4Amount == 0
                            && ovm.PS3Amount == 0 && ovm.XB360Amount == 0;

            if ( !ModelState.IsValid || notAllZero )
            {
                TempData["Customer"] = customerId;
                TempData["Store"] = storeId;
                ViewData["Customer"] = customerId;
                ViewData["Store"] = storeId;
                return View(ovm);
            }

            List<int> amount = new List<int>();
            decimal totalPrice = 0;
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        amount.Add(ovm.NSAmount);
                        totalPrice += products[i].UnitPrice * ovm.NSAmount;
                        break;
                    case 1:
                        amount.Add(ovm.PS4PAmount);
                        totalPrice += products[i].UnitPrice * ovm.PS4PAmount;
                        break;
                    case 2:
                        amount.Add(ovm.XBOAmount);
                        totalPrice += products[i].UnitPrice * ovm.XBOAmount;
                        break;
                    case 3:
                        amount.Add(ovm.PS4Amount);
                        totalPrice += products[i].UnitPrice * ovm.PS4Amount;
                        break;
                    case 4:
                        amount.Add(ovm.PS3Amount);
                        totalPrice += products[i].UnitPrice * ovm.PS3Amount;
                        break;
                    default:
                        amount.Add(ovm.XB360Amount);
                        totalPrice += products[i].UnitPrice * ovm.XB360Amount;
                        break;
                }
            }

            Order order = new Order
            {
                CustomerId = customerId,
                Amount = amount,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                StoreId = storeId
            };

            int orderId = iRepo.OrderPlaced(order);

            if (orderId > -1)
            {
                Receipt receipt = new Receipt
                {
                    Order = order,
                    orderId = orderId
                };
                ViewData["Receipt"] = receipt;
                return View("OrderComplete", receipt);
            }
            else
            {
                ViewData["Price"] = price;
                TempData["Customer"] = customerId;
                TempData["Store"] = storeId;
                ViewData["Customer"] = customerId;
                ViewData["Store"] = storeId;
                return View(ovm);
            }
        }
        /// <summary>
        /// GET show the order reciept to customer
        /// then go back to main page
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderComplete()
        {
            return View("Index");
        }
    }
}