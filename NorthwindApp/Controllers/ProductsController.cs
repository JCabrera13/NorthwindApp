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

        public async Task<IActionResult> EditarProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Supplier = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(product);
        }

        [HttpPost]
        public IActionResult EditarProducto(int id, [Bind()] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Supplier = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(product);
        }

    }
    }
