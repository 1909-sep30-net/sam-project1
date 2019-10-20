using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using d = DB.Entities;
using l = GStoreApp.Library;
using NLog;

namespace DB.Repo
{   
    //Connect UI with database, but not business logic
    public class Repo : l.IRepo
    {   
        private static d.GCStoreContext dbcontext;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Start up a new Database Context controller
        /// </summary>
        public Repo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<d.GCStoreContext>();
            optionsBuilder.UseSqlServer(Config.connectionString);
            dbcontext = new d.GCStoreContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Function to add new customer into database 
        /// via the parameter: Customer object from UI
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(l.Customer customer)
        {
            //Add Customer Data into database

            d.Customer entity = Mapper.MapCustomer(customer);
            dbcontext.Add(entity);
            dbcontext.SaveChanges();
            logger.Info("Added customer into Customer table.");
        }

        /// <summary>
        /// Function to search current customer in database
        /// then pass the result back to UI
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>IEnumerable Collection of Library.Customer object</returns>
        public IEnumerable<l.Customer> SearchCustomer( l.Customer customer )
        {
            //Search Customer from database
            IQueryable<d.Customer> cusotmerFound
                = dbcontext.Customer.Where(c => c.LastName == customer.LastName
                                           && c.FirstName == customer.FirstName
                                           && c.PhoneNumber == customer.PhoneNumber);

            // IEnumerable<l.Customer> customerFound = Mapper.MapCustomer(entity);
            
            return cusotmerFound.Select(Mapper.MapCustomer);
        }

        /// <summary>
        /// Function to Add new order into datebase include: OrderOverView, OrderItem
        /// And Update Inventory for specific store
        /// </summary>
        /// <param name="order"></param>
        /// <returns>order success or fail message</returns>
        public string OrderPlaced( l.Order order )
        {
            IQueryable<d.Inventory> CurrentInventoryQ
                = dbcontext.Inventory.Where(i => i.StoreId == order.StoreId)
                                     .AsNoTracking();
            IEnumerable<l.Inventory> CurrentInventoryE = CurrentInventoryQ.Select(Mapper.MapInventory);
            List < l.Inventory > invent = CurrentInventoryE.ToList();
            for ( int j = 0; j < CurrentInventoryE.Count(); j++)
            {
                if ( invent[j].Amount - order.Amount[j] < 0 )
                {
                    return "Sorry! We don't have enough items in our storage.";
                }
                else
                {
                    var inventory = dbcontext.Inventory.Where(i => i.ProductId == j + 1)
                             .First();
                    inventory.Amount = invent[j].Amount - order.Amount[j];
                    if ( j == CurrentInventoryE.Count() - 1)
                    {
                        dbcontext.SaveChanges();
                        logger.Info("Inventory Updated!");
                    }
                }
            }

            d.OrderOverView orderOverView = Mapper.MapOrderOverView(order);
            dbcontext.Add(orderOverView);
            logger.Info("A new order overview is added into database");
            dbcontext.SaveChanges();

            int orderId = orderOverView.OrderId;

            string productName;
            int orderAmount;
            IQueryable < d.Product > products = dbcontext.Product;
            List<l.Product> libraryProducts = products.Select(Mapper.MapProduct).ToList();
            for( int i = 0; i < 6; i++)
            {
                if ( order.Amount[i] != 0)
                {
                    orderAmount = order.Amount[i];
                    productName = libraryProducts[i].ProductName;
                    d.OrderItem item
                        = Mapper.MapOrderItem(orderAmount, orderId, productName);
                    dbcontext.Add(item);
                }
            }

            dbcontext.SaveChanges();
            return $"Order Success!! Your order Id is: {orderId}.";

        }

        /// <summary>
        /// Function to search past one order detail via order id
        /// it will search from data table: OrderOverview
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public l.OrderOverView SearchPastOrder(int orderid)
        {
            d.OrderOverView overView = dbcontext.OrderOverView.Find(orderid);
            if ( overView == null)
            {
                return null;
            } else
            {
                l.OrderOverView overView1 = Mapper.MapOrderOverView(overView);
                return overView1;
            }
            
        }

        /// <summary>
        /// Function to search past one order detail via order id
        /// it will search from data table: OrderItem
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public IEnumerable<l.OrderItem> SearchPastOrderItem(int orderid)
        {
            IQueryable<d.OrderItem> orderItems
                = dbcontext.OrderItem.Where( o => o.OrderId == orderid )
                                     .AsNoTracking();
            return orderItems.Select(Mapper.MapOrderItem);
        }

        /// <summary>
        /// search the database and pass the order history back to UI via store ID
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public IEnumerable<l.OrderOverView> DisplayOrderByStore ( int storeId )
        {
            //search order by store id
            IQueryable<d.OrderOverView> orderHistory
                = dbcontext.OrderOverView.Where(o => o.StoreId == storeId)
                                         .AsNoTracking();
            return orderHistory.Select(Mapper.MapOrderOverView);
        }

        /// <summary>
        /// search the database and pass the order history back to UI via store ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<l.OrderOverView> DisplayOrderByCustomer( int customerId )
        {
            //search order by customer id
            IQueryable<d.OrderOverView> orderHistory
                = dbcontext.OrderOverView.Where(o => o.CustomerId == customerId)
                                         .AsNoTracking();
            return orderHistory.Select(Mapper.MapOrderOverView);
        }

        /// <summary>
        /// while adding new customer, search database to ckeck if the store is existed
        /// when the customer type in favorite store
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public l.Store CheckIfStoreExists( int storeId )
        {
            d.Store store = dbcontext.Store.Find(storeId);
            if (store == null)
            {
                return null;
            }
            else
            {
                l.Store storeFind = Mapper.MapStore(store);
                return storeFind;
            }
        }

        /// <summary>
        /// while placing an order, search database to retrieve current product and price
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<l.Product> SearchProduct()
        {
             IQueryable<d.Product> product = dbcontext.Product;
             if (product == null)
             {
                 return null;
             }
             else
             {
                 return product.Select(Mapper.MapProduct);
             }
        }
    }
}
