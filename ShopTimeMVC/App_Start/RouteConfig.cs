using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopTimeMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "men",
                url: "men",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "women",
                url: "women",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "kids",
                url: "kids",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "home",
                url: "home",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "clearcart",
                url: "clearcart",
                defaults: new { controller = "Cart", action = "ClearCart", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "updatecart",
                url: "updatecart/{productId}/{recordId}/{count}",
                defaults: new { controller = "Cart", action = "UpdateCartItemCount", productId = UrlParameter.Optional, recordId = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "cart",
                url: "cart",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "checkout",
                url: "checkout/{order}",
                defaults: new { controller = "Cart", action = "Checkout", order = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "complete",
                url: "complete",
                defaults: new { controller = "Cart", action = "Complete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "addtocart",
                url: "addtocart/{id}",
                defaults: new { controller = "Cart", action = "AddToCart", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "remove",
                url: "remove/{id}",
                defaults: new { controller = "Cart", action = "RemoveFromCart", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
