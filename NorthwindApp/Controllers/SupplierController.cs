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
            return View(db.Suppliers);
        }

        public IActionResult NuevoProveedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuevoProveedor(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //guardar en la base de datos
                db.Add(supplier);
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }


        public IActionResult EliminarProveedor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var proveedor = db.Suppliers.Find(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        public IActionResult ConfirmarEliminarProveedor(int SupplierId)
        {
            var proveedor = db.Suppliers.Find(SupplierId);
            var productos = db.Products.Where(p => p.SupplierId == SupplierId).ToList();

            if(productos == null)
            {
                if (proveedor == null)
                {
                    return NotFound();
                }
                db.Suppliers.Remove(proveedor);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensaje = "No se puede eliminar el proveedor por que tiene asociados productos";
            return View("EliminarProveedor",proveedor);
          
        }

    }
}
