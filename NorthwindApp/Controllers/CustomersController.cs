using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.Models;

namespace NorthwindApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly NorthwindContext db;

        public CustomersController(NorthwindContext context)
        {
            db = context;
        }

        // GET: Customers
        public IActionResult Index()
        {
            var listadoClientes = db.Customers.ToList();
            return View(listadoClientes);
        }

        public IActionResult NuevoCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuevoCliente([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")]Customer customer)
        {
            if (ModelState.IsValid)
            {
                //agregar a la bd
                db.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(customer);
            
        }

        public IActionResult EditarCliente(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var cliente = db.Customers.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult EditarCliente(string id, Customer customer)
        {
            
            //se revisa si el id que se manda es el mismo al de la url
            if (id != customer.CustomerId)
            {
                return NotFound();
            }
            //validar si el modelo recibido es valido
            if (ModelState.IsValid)
            {
                //se mantiene en cola y se actualiza en la bd
                db.Update(customer);
                //se guardan cambios
                db.SaveChanges();
                //manda a index
                return RedirectToAction(nameof(Index));
            }


            return View(customer);
        }


    }
}
