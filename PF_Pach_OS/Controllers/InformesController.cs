﻿using Microsoft.AspNetCore.Mvc;

namespace PF_Pach_OS.Controllers
{
    public class InformesController : Controller
    {
        public async Task <IActionResult> Index()
        {
            return View();
        }
    }
}