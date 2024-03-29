﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GStoreApp.Library;
using GStore.WebUI.Models;
using NLog; 

namespace GStore.WebUI.Controllers
{
    public class StuffController : Controller
    {
        private readonly IRepo iRepo;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public StuffController(IRepo repository)
        {
            iRepo = repository;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// call repository to check if the login info is correct
        /// if correct then go into search menu page
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index( GStoreApp.Library.Login login )
        {
            int pass = iRepo.Login(login);

            if (pass == 1)
            {
                return View("Menu");
            } 
            else
            {
                return View("Index");
            }
        }

        /// <summary>
        /// GET, default search menu page
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }

        /// <summary>
        /// retrieve input number and call
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Menu( CheckType type )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Error($"Model State is invalid");
                    return View();
                }
                else if (type.OrderId != 0)
                {
                    OrderOverView o = iRepo.SearchPastOrder(type.OrderId);
                    if (o == null)
                    {
                        return View(type);
                    }
                    List<OrderItem> orderItem = iRepo.SearchPastOrderItem(type.OrderId).ToList();
                    List<string> productName = new List<string>();
                    List<int> amount = new List<int>();

                    foreach (OrderItem item in orderItem)
                    {
                        productName.Add(item.ProductName);
                        amount.Add(item.Amount);
                    }
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = o.OrderId,
                        CustomerId = o.CustomerId,
                        StoreId = o.StoreId,
                        OrderDate = o.OrderDate,
                        ProductName = productName,
                        Amount = amount,
                        TotalPrice = o.TotalPrice
                    };
                    return View("OrderDetail", orderDetail);
                }
                else if (type.StoreId != 0)
                {
                    List<OrderOverView> overViews =
                        iRepo.DisplayOrderByStore(type.StoreId).ToList();
                    if (overViews.Count == 0)
                    {
                        return View(type);
                    }
                    List<int> oId = new List<int>();
                    List<int> cId = new List<int>();
                    List<int> sId = new List<int>();
                    List<DateTime> dates = new List<DateTime>();
                    List<decimal> p = new List<decimal>();
                    for (int i = 0; i < overViews.Count; i++)
                    {
                        oId.Add(overViews[i].OrderId);
                        cId.Add(overViews[i].CustomerId);
                        sId.Add(overViews[i].StoreId);
                        dates.Add(overViews[i].OrderDate);
                        p.Add(overViews[i].TotalPrice);
                    }
                    DisplayOrder displayOrder = new DisplayOrder
                    {
                        OrderId = oId,
                        CustomerId = cId,
                        StoreId = sId,
                        OrderDate = dates,
                        TotalPrice = p
                    };
                    ViewData["display"] = displayOrder;
                    return View("OrderHistory", displayOrder);
                }
                else if (type.CustomerId != 0)
                {
                    List<OrderOverView> overViews =
                        iRepo.DisplayOrderByCustomer(type.CustomerId).ToList();
                    if (overViews.Count == 0)
                    {
                        return View(type);
                    }
                    List<int> oId = new List<int>();
                    List<int> cId = new List<int>();
                    List<int> sId = new List<int>();
                    List<DateTime> dates = new List<DateTime>();
                    List<decimal> p = new List<decimal>();
                    for (int i = 0; i < overViews.Count; i++)
                    {
                        oId.Add(overViews[i].OrderId);
                        cId.Add(overViews[i].CustomerId);
                        sId.Add(overViews[i].StoreId);
                        dates.Add(overViews[i].OrderDate);
                        p.Add(overViews[i].TotalPrice);
                    }
                    DisplayOrder displayOrder = new DisplayOrder
                    {
                        OrderId = oId,
                        CustomerId = cId,
                        StoreId = sId,
                        OrderDate = dates,
                        TotalPrice = p
                    };
                    ViewData["display"] = displayOrder;
                    return View("OrderHistory", displayOrder);
                }
            }
            catch ( InvalidOperationException ex )
            {
                logger.Error($"StuffController: {ex.Message}");
                return View(type);
            }

            return View();
        }

        public ActionResult OrderDetail()
        {
            return View();
        }

    }
}