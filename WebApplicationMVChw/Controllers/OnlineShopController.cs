using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationMVChw.Models;

namespace WebApplicationMVChw.Controllers
{
    public class OnlineShopController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Phone", Price = 1000 },
            new Product { Id = 2, Name = "Laptop", Price = 2000 },
            new Product { Id = 3, Name = "Tablet", Price = 1500 }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
            }

            return RedirectToAction("Index");
        }
    }

}
