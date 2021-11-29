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

        public async Task<IActionResult> EditarCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult EditarCategoria(int id,Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //actualizar en la bd
                db.Update(category);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return View(category);
        }

        public IActionResult EliminarCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = db.Categories.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult ConfirmacionEliminarCategoria(int CategoryId)
        {
            var categoria = db.Categories.Find(CategoryId);

            if(categoria == null)
            {
                return NotFound();
            }

            db.Remove(categoria);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}