using ShopTimeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTimeMVC.Controllers
{
    public class StoreController : Controller
    {
        Product productService = new Product();
        // GET: Men
        public ActionResult Index()
        {
            var view = HttpContext.Request.Url.AbsolutePath.ToString().Substring(1);

            return View(productService.GetProductsByGender(view));
        }
    }
}