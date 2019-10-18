using System;
using System.Collections.Generic;

namespace GStoreApp.Library
{
    public class Product
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public Product() { }

        public Product(string pName, decimal price)
        {
            ProductName = pName;
            UnitPrice = price;
        }
    }
}
