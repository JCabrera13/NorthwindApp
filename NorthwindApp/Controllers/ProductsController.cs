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
            public async Task<IActionResult> Index()
            {
                var listadoProductos = db.Products;
                return View(await listadoProductos.ToListAsync());
            }
        }
    }
