using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class ProductController : Controller
    {
        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(int CategoryID=0)
        {
            if (CategoryID != 0)
            {
                ViewBag.TieuDe = db.Categories.SingleOrDefault(p => p.Name != null).Name;
                var model = db.Products.Where(p => p.CategoryId == CategoryID);
                return View(model);
            }

            return View();
        }



        public ActionResult Detail(int id, string SupplierID)
        {
            var model = db.Products.Find(id);

            // Increase the number of views
            model.Views++;
            db.SaveChanges();

            // Get the old cookie named views
            var views = Request.Cookies["views"];
            // If there is no old cookie -> create a new one
            if (views == null)
            {
                views = new HttpCookie("views");
            }
            // Add viewed item to cookie
            views.Values[id.ToString()] = id.ToString();
            // Set cookie lifetime
            views.Expires = DateTime.Now.AddHours(24);
            // Send cookies to client to save
            Response.Cookies.Add(views);

            // Get List<int> containing viewed item code from cookie
            var keys = views.Values
                .AllKeys.Select(k => int.Parse(k)).ToList();
            // Query viewed rows
            ViewBag.Views = db.Products
                .Where(p => keys.Contains(p.Id));
            ViewBag.luotxem = db.Products
                .Where(p => keys.Contains(p.Id));
            // Top 10 Product
            ViewBag.Top = db.Products.Where(p => p.Id > 0).OrderByDescending(p => p.Views).Take(10).ToList();
            return View(model);
        }
    }
}