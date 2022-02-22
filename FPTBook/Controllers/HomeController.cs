using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Category()
        {
            var model = db.Categories;
            return PartialView("_Category", model);
        }
        public ActionResult SaleOff()
        {
            var model = db.Products.Where(p=>p.Discount>0).OrderBy(p=>Guid.NewGuid()).Take(1);
            return PartialView("_SaleOff", model);
        }
        public ActionResult Special()
        {
            var model = db.Products.Where(p => p.Special ==true);
            return PartialView("_BestSeller", model);
        }
    }
}