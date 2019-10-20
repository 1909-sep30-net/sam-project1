using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GStoreApp.Library;


namespace DB.Repo
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public static class Mapper
    {

        public static Customer MapCustomer(Entities.Customer customer)
        {
            return new Customer
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FavoriteStore = customer.FavoriteStore,
                PhoneNumber = customer.PhoneNumber,
            };
        }

        public static Entities.Customer MapCustomer(Customer customer)
        {
            return new Entities.Customer
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FavoriteStore = customer.FavoriteStore,
                PhoneNumber = customer.PhoneNumber,
            };
        }

        public static Inventory MapInventory( Entities.Inventory i )
        {
            return new Inventory
            {
                InventoryId = i.InventoryId,
                StoreId = i.StoreId,
                ProductId = i.ProductId,
                Amount = i.Amount
            };
        }

        public static Entities.Inventory MapInventory(Inventory i)
        {
            return new Entities.Inventory
            {
                InventoryId = i.InventoryId,
                StoreId = i.StoreId,
                ProductId = i.ProductId,
                Amount = i.Amount
            };
        }

        public static OrderItem MapOrderItem( Entities.OrderItem or)
        {
            return new OrderItem
            {
                OrderItemId = or.OrderItemId,
                OrderId = or.OrderId,
                ProductName = or.ProducutName,
                Amount = or.Amount
            };
        }

        public static Entities.OrderItem MapOrderItem( int amount, int orderId, string product )
        {
            return new Entities.OrderItem
            {
                OrderItemId = 0,
                OrderId = orderId,
                ProducutName = product,
                Amount = amount
            };
        }

        public static OrderOverView MapOrderOverView(Entities.OrderOverView ov)
        {
            return new OrderOverView
            {
                OrderId = ov.OrderId,
                StoreId = ov.StoreId,
                OrderDate = ov.OrderDate,
                CustomerId = ov.CustomerId,
                TotalPrice = ov.TotalPrice
            };
        }

        public static Entities.OrderOverView MapOrderOverView( Order o )
        {
            return new Entities.OrderOverView
            {
                OrderId = 0,
                StoreId = o.StoreId,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                TotalPrice = o.TotalPrice
            };
        }

        public static Entities.OrderOverView MapOrderOverView( int orderId )
        {
            return new Entities.OrderOverView
            {
                OrderId = orderId
            };
        }

        public static Product MapProduct(Entities.Product p)
        {
            return new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice
            };
        }

        public static Entities.Product MapProduct(Product p)
        {
            return new Entities.Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice
            };
        }

        public static Store MapStore(Entities.Store s)
        {
            return new Store
            {
                StoreId = s.StoreId,
                StoreName = s.StoreName,
                City = s.City,
                Postal = s.Postal
            };
        }

        public static Entities.Store MapStore(Store s)
        {
            return new Entities.Store
            {
                StoreId = s.StoreId,
                StoreName = s.StoreName,
                City = s.City,
                Postal = s.Postal
            };
        }
    }
}
