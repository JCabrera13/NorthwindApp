using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly NorthwindContext db;

        public SuppliersController(NorthwindContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var listadoSuppliers = db.Suppliers.ToList();
            return View();
        }
    }
}
