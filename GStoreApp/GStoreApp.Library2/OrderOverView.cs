using System;
using System.Collections.Generic;

namespace GStoreApp.Library
{
    public class OrderOverView
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

    }


}
