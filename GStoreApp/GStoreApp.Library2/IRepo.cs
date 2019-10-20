using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library
{
    public interface IRepo
    {
        public void AddCustomer(Customer customer);

        public IEnumerable<Customer> SearchCustomer(Customer customer);

        public string OrderPlaced(Order order);

        public OrderOverView SearchPastOrder(int orderid);

        public IEnumerable<OrderItem> SearchPastOrderItem(int orderid);

        public IEnumerable<OrderOverView> DisplayOrderByStore(int storeId);

        public IEnumerable<OrderOverView> DisplayOrderByCustomer(int customerId);

        public Store CheckIfStoreExists(int storeId);

        public IEnumerable<Product> SearchProduct();
    }
}
