using System;
using System.Collections.Generic;

namespace GStoreApp.Library
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }

    }
}
