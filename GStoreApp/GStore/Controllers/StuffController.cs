﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GStoreApp.Library; 

namespace GStore.WebUI.Controllers
{
    public class StuffController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
    }
}