﻿using System;
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

        //[HttpPost]
        //public IActionResult NuevoCliente(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //agregar a la bd

        //    }
        //    else
        //    {
        //        return View(customer);
        //    }
        //}
    }
}