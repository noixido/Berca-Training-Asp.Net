﻿using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = "departmentActivePage";
            return View();
        }
    }
}
