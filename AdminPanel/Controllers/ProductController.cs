using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        public readonly ApplicationDbContext db;

        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        public IActionResult EditOrDelete()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return RedirectToAction("EditOrDelete", "Product");
            }
            var product = db.Products.Find(id); 
            if(product == null)
            {
                return RedirectToAction("EditOrDelete", "Product");
            }
            return View(product); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Update(product);
                db.SaveChanges();
                return RedirectToAction("EditOrDelete", "Product");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction("EditOrDelete", "Product");
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("EditOrDelete", "Product");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("EditOrDelete", "Product");
        }
    }
}
