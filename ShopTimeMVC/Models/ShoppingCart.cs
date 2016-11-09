using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTimeMVC.Models
{
    public partial class ShoppingCart
    {
        ShopTimeEntities shopTimeDB = new ShopTimeEntities();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(Product product)
        {
            // Get the matching cart and product instances
            var cartItem = shopTimeDB.Carts.SingleOrDefault(
c => c.CartId == ShoppingCartId
&& c.ProductId == product.Id);

            int itemCount = 0;

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                itemCount = cartItem.Count;

                shopTimeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;

                itemCount = cartItem.Count;
            }

            // Save changes
            shopTimeDB.SaveChanges();

            return itemCount;
        }

        public void UpdateCartItemCount(Product product, int count)
        {
            // Get the matching cart and product instances
            var cartItem = shopTimeDB.Carts.SingleOrDefault(
c => c.CartId == ShoppingCartId
&& c.ProductId == product.Id);

            

            if (cartItem != null)
            {
                // If the item does exist in the cart, then update quantity
                cartItem.Count = count;
            }

            // Save changes
            shopTimeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = shopTimeDB.Carts.Single(
cart => cart.CartId == ShoppingCartId
&& cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                shopTimeDB.Carts.Remove(cartItem);

                // Save changes
                shopTimeDB.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = shopTimeDB.Carts.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                shopTimeDB.Carts.Remove(cartItem);
            }

            // Save changes
            shopTimeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return shopTimeDB.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in shopTimeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply product price by count of that product to get 
            // the current price for each of those products in the cart
            // sum all product price totals to get the cart total
            decimal? total = (from cartItems in shopTimeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal subTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count,
                    Product = item.Product
                };

                // Set the order total of the shopping cart
                subTotal += (item.Count * item.Product.Price);

                shopTimeDB.OrderDetails.Add(orderDetail);

            }

            // Set the order's sub total
            order.SubTotal = subTotal;

            var province = (ProvinceType)Enum.Parse(typeof(ProvinceType), order.Province);

            switch (province)
            {
                case ProvinceType.AB:
                    order.Tax = Math.Round(order.SubTotal * 0.05M, 2);
                    break;
                case ProvinceType.BC:
                    order.Tax = Math.Round(order.SubTotal * 0.05M, 2);
                    break;
                case ProvinceType.MB:
                    order.Tax = Math.Round(order.SubTotal * 0.05M, 2);
                    break;
                case ProvinceType.NB:
                    order.Tax = Math.Round(order.SubTotal * 0.13M, 2);
                    break;
                case ProvinceType.NL:
                    order.Tax = Math.Round(order.SubTotal * 0.13M, 2);
                    break;
                case ProvinceType.NS:
                    order.Tax = Math.Round(order.SubTotal * 0.15M, 2);
                    break;
                case ProvinceType.ON:
                    order.Tax = Math.Round(order.SubTotal * 0.13M, 2);
                    break;
                case ProvinceType.PE:
                    order.Tax = Math.Round(order.SubTotal * 0.14M, 2);
                    break;
                case ProvinceType.QB:
                    order.Tax = Math.Round(order.SubTotal * 0.05M, 2);
                    break;
                case ProvinceType.SK:
                    order.Tax = Math.Round(order.SubTotal * 0.05M, 2);
                    break;
                case ProvinceType.YK:
                    order.Tax = Math.Round(order.SubTotal * 0.05M, 2);
                    break;
            }

            // shipping and expected delivery date
            if (order.SubTotal > 0 && order.SubTotal < 25)
            {
                order.Shipping = 3.00M;
                order.ExpectedDeliveryDate = DateTime.Now.AddDays(1);
            }
            else if (order.SubTotal > 25.01M && order.SubTotal < 50)
            {
                order.Shipping = 4.00M;
                order.ExpectedDeliveryDate = DateTime.Now.AddDays(3);
            }
            else if (order.SubTotal > 50.01M && order.SubTotal < 75)
            {
                order.Shipping = 5.00M;
                order.ExpectedDeliveryDate = DateTime.Now.AddDays(1);
            }
            else if (order.SubTotal > 75)
            {
                order.Shipping = 6.00M;
                order.ExpectedDeliveryDate = DateTime.Now.AddDays(4);
            }

            order.Total = order.SubTotal + order.Tax + order.Shipping;


            // Save the order
            shopTimeDB.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }
        
    }
}