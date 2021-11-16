using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    namespace NorthwindApp.Controllers
    {
        public class ProductsController : Controller
        {
            private readonly NorthwindContext db;

            public ProductsController(NorthwindContext context)
            {
                db = context;
            }
            public IActionResult Index()
            {
            var listadoProductos = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(listadoProductos);    
            }
        }
    }
