using ShopTimeMVC.Models;
using ShopTimeMVC.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShopTimeMVC.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedProduct = shopTimeDB.Products
                .Single(product => product.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            int itemCount = cart.AddToCart(addedProduct);

            string productName = shopTimeDB.Products
                .Single(item => item.Id == id).Name;

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been added to your cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult UpdateCartItemCount(int productId,int recordId, int count)
        {
            // Retrieve the album from the database
            var addedProduct = shopTimeDB.Products
                .Single(product => product.Id == productId);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.UpdateCartItemCount(addedProduct, count);

            return Json(Url.Action("Index", "Cart"));
        }

        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string product = shopTimeDB.Carts
                .Single(item => item.RecordId == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(product) +
                    " has been removed from your cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(Url.Action("Index", "Cart"));
        }

        public ActionResult ClearCart()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmptyCart();

            return Json(Url.Action("Index", "Cart"));
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount() > 0 ? cart.GetCount().ToString() : string.Empty;

            return PartialView("CartSummary");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            order.ExpectedDeliveryDate = DateTime.Now;

            //Save Order
            shopTimeDB.Orders.Add(order);
            shopTimeDB.SaveChanges();

            //Process the order
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            return Json(Url.Action("complete",
                        new { id = order.OrderId }));
        }

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            var order = shopTimeDB.Orders.Where(
                o => o.OrderId == id).FirstOrDefault();

            var orderItems = shopTimeDB.OrderDetails.Where(
                o => o.OrderId == id).ToList();

            order.OrderDetails = orderItems;

            return View(order);
        }
    }
}