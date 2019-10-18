using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library
{
    public class Order
    {
        public Customer Customer { get; set; }
        public int NSAmount { get; set; }
        public int XBAmount { get; set; }
        public int PSAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int StoreId { get; set; }

        public Order(Customer customer, int nSAmount, int xBAmount, int pSAmount,
                     DateTime orderDate, decimal totalPrice, int storeId)
        {
            Customer = customer;
            NSAmount = nSAmount;
            XBAmount = xBAmount;
            PSAmount = pSAmount;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            StoreId = storeId;
        }
    }
  
}
