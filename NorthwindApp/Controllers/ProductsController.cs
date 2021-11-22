using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            public IActionResult NuevoProducto()
        {
            ViewBag.Supplier = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        public IActionResult NuevoProducto(Product product)
        {
            if (ModelState.IsValid)
            {
                //insertar en la bd
                db.Add(product);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(product);

        }

    }
    }
