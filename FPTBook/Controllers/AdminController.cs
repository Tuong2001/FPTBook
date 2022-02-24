using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class AdminController : Controller
    {
        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product()
        {
            var products = db.Products.ToList();
            return View(products);
        }

    }
}