using ShopTimeMVC.Models;
using System.Collections.Generic;

namespace ShopTimeMVC.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}