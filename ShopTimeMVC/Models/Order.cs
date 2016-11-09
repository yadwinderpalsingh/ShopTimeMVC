using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTimeMVC.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Shipping { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}