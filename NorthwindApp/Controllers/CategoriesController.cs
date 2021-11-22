using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext db;
        public CategoriesController(NorthwindContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var listadoCategorias = db.Categories.ToList();
            return View(listadoCategorias);
        }
        public IActionResult NuevaCategoria()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevaCategoria(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}