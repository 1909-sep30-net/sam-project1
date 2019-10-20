using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library
{
    public class Order
    {
        public int CustomerId { get; set; }
        public List<int> Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int StoreId { get; set; }


    }
  
}
