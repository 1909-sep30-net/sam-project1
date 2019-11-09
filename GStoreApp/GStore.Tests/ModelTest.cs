using System;
using System.Collections.Generic;
using System.Text;
using m = GStore.WebUI.Models;
using l = GStoreApp.Library;
using d = DB.Entities;
using Xunit;

namespace GStore.Tests
{
    public class MvcModelTest
    {
        [Fact]
        public void CustomerTest()
        {
            m.CustomerViewModel customer = new m.CustomerViewModel
            {
                CustomerId = 1,
                FirstName = "Sam",
                LastName = "Lin",
                Phone = "(123)456-7892",
                FavoriteStore = 1
            };

            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("Sam", customer.FirstName);
            Assert.Equal("Lin", customer.LastName);
            Assert.Equal("(123)456-7892", customer.Phone);
            Assert.Equal(1, customer.FavoriteStore);
        }

        [Fact]
        public void CheckTypeTest()
        {
            m.CheckType check = new m.CheckType
            {
                CustomerId = 44,
                OrderId = 10,
                StoreId = 3
            };

            Assert.Equal(44, check.CustomerId);
            Assert.Equal(10, check.OrderId);
            Assert.Equal(3, check.StoreId);
        }

        [Fact]
        public void DisplayOrderTest()
        {
            List<DateTime> dateList = new List<DateTime>();
            DateTime date = Convert.ToDateTime("01/01/2008");
            dateList.Add(date);

            m.DisplayOrder display = new m.DisplayOrder
            {
                OrderId = new List<int> {1},
                CustomerId = new List<int> {1},
                StoreId = new List<int> {1},
                OrderDate = dateList,
                TotalPrice = new List<decimal> {(decimal)99.99}
            };

            Assert.Equal(1, display.OrderId[0]);
            Assert.Equal(1, display.CustomerId[0]);
            Assert.Equal(1, display.OrderId[0]);
            Assert.Equal(date, display.OrderDate[0]);
            Assert.Equal((decimal)99.99, display.TotalPrice[0]);
        }

        [Fact]
        public void LoginTest()
        {
            m.Login login = new m.Login
            {
                Name = "Sam",
                Password = 1234
            };

            Assert.Equal("Sam", login.Name);
            Assert.Equal(1234, login.Password);
        }

        public void OrderDetailTest()
        {
            List<string> pn = new List<string>();
            pn.Add("NS");
            List<int> am = new List<int>();
            am.Add(1);
            DateTime date = Convert.ToDateTime("2009/01/01");

            m.OrderDetail detail = new m.OrderDetail
            {
                CustomerId = 1,
                OrderId = 1,
                ProductName = pn,
                Amount = am,
                StoreId = 2,
                OrderDate = date
            };

            Assert.Equal(1, detail.CustomerId);
            Assert.Equal(1, detail.OrderId);
            Assert.Equal("NS", pn[0]);
            Assert.Equal(1, am[0]);
            Assert.Equal(date, detail.OrderDate);
            Assert.Equal(2, detail.StoreId);
        }

        [Fact]
        public void OrderOverViewTest()
        {
            m.OrderViewModel order = new m.OrderViewModel
            {
                NSAmount = 1,
                PS4PAmount = 2,
                XBOAmount = 3,
                PS4Amount = 4,
                PS3Amount = 5,
                XB360Amount = 6
            };

            Assert.Equal(1, order.NSAmount);
            Assert.Equal(2, order.PS4PAmount);
            Assert.Equal(3, order.XBOAmount);
            Assert.Equal(4, order.PS4Amount);
            Assert.Equal(5, order.PS3Amount);
            Assert.Equal(6, order.XB360Amount);
        }

        [Fact]
        public void PriceViewModelTest()
        {
            List<decimal> price = new List<decimal>();
            price.Add((decimal)123.99);

            m.PriceViewModel tPrice = new m.PriceViewModel
            {
                Price = price
            };

            Assert.Equal((decimal)123.99, tPrice.Price[0]);
        }

        [Fact]
        public void ProductViewTest()
        {
            m.ProductViewModel product = new m.ProductViewModel
            {
                ProductId = 1,
                ProductName = "NS",
                UnitPrice = (decimal)99.99
            };

            Assert.Equal(1, product.ProductId);
            Assert.Equal("NS", product.ProductName);
            Assert.Equal((decimal)99.99, product.UnitPrice);
        }

        [Fact]
        public void ReceiptTest()
        {
            m.Receipt receipt = new m.Receipt
            {
                orderId = 2,
            };

            Assert.Equal(2, receipt.orderId);
        }
    }

    public class LogicModelTest
    {
        [Fact]
        public void CustomerTest()
        {
            l.Customer customer = new l.Customer
            {
                CustomerId = 1,
                FirstName = "Sam",
                LastName = "Lin",
                FavoriteStore = 1,
                PhoneNumber = "1234567890"
            };

            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("Sam", customer.FirstName);
            Assert.Equal("Lin", customer.LastName);
            Assert.Equal(1, customer.FavoriteStore);
            Assert.Equal("1234567890", customer.PhoneNumber);
        }

        [Fact]
        public void InventoryTest()
        {
            l.Inventory invent = new l.Inventory
            {
                InventoryId = 1,
                ProductId = 2,
                StoreId = 3,
                Amount = 3
            };

            Assert.Equal(1, invent.InventoryId);
            Assert.Equal(2, invent.ProductId);
            Assert.Equal(3, invent.StoreId);
            Assert.Equal(3, invent.Amount);
        }

        [Fact]
        public void LoginTest()
        {
            l.Login login = new l.Login
            {
                Name = "Lin",
                Password = 1234
            };

            Assert.Equal("Lin", login.Name);
            Assert.Equal(1234, login.Password);
        }

        [Fact]
        public void OrderTest()
        {
            List<int> amount = new List<int> { 1 };
            DateTime date = Convert.ToDateTime("2008/01/01");

            l.Order order = new l.Order
            {
                CustomerId = 1,
                StoreId = 2,
                Amount = amount,
                TotalPrice = (decimal)299.99,
                OrderDate = date
            };

            Assert.Equal(1, order.CustomerId);
            Assert.Equal(2, order.StoreId);
            Assert.Equal(1, order.Amount[0]);
            Assert.Equal((decimal)299.99, order.TotalPrice);
            Assert.Equal(date, order.OrderDate);
        }

        [Fact]
        public void OrderItemTest()
        {
            l.OrderItem order = new l.OrderItem
            {
                OrderItemId = 1,
                OrderId = 3,
                Amount = 6,
                ProductName = "NS"
            };

            Assert.Equal(1, order.OrderItemId);
            Assert.Equal(3, order.OrderId);
            Assert.Equal(6, order.Amount);
            Assert.Equal("NS", order.ProductName);
        }

        [Fact]
        public void ProductTest()
        {
            l.Product product = new l.Product
            {
                ProductId = 1,
                ProductName = "NS",
                UnitPrice = (decimal)299.99
            };

            Assert.Equal(1, product.ProductId);
            Assert.Equal("NS", product.ProductName);
            Assert.Equal((decimal)299.99, product.UnitPrice);
        }

        [Fact]
        public void StoreTest()
        {
            l.Store store = new l.Store
            {
                StoreId = 1,
                StoreName = "GCS",
                City = "AR",
                Postal = "76010"
            };

            Assert.Equal(1, store.StoreId);
            Assert.Equal("GCS", store.StoreName);
            Assert.Equal("AR", store.City);
            Assert.Equal("76010", store.Postal);
        }
    }

    public class EntityModelTest
    {
        [Fact]
        public void CustomerTest()
        {
            d.Customer customer = new d.Customer
            {
                CustomerId = 1,
                FirstName = "Sam",
                LastName = "Lin",
                FavoriteStore = 1,
                PhoneNumber = "1234567890"
            };

            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("Sam", customer.FirstName);
            Assert.Equal("Lin", customer.LastName);
            Assert.Equal(1, customer.FavoriteStore);
            Assert.Equal("1234567890", customer.PhoneNumber);
        }

        [Fact]
        public void InventoryTest()
        {
            d.Inventory invent = new d.Inventory
            {
                InventoryId = 1,
                ProductId = 2,
                StoreId = 3,
                Amount = 3
            };

            Assert.Equal(1, invent.InventoryId);
            Assert.Equal(2, invent.ProductId);
            Assert.Equal(3, invent.StoreId);
            Assert.Equal(3, invent.Amount);
        }

        [Fact]
        public void LoginTest()
        {
            d.Login login = new d.Login
            {
                Name = "Lin",
                Password = 1234
            };

            Assert.Equal("Lin", login.Name);
            Assert.Equal(1234, login.Password);
        }

        [Fact]
        public void OrderTest()
        {
            List<int> amount = new List<int> { 1 };
            DateTime date = Convert.ToDateTime("2008/01/01");

            d.OrderOverView order = new d.OrderOverView
            {
                CustomerId = 1,
                StoreId = 2,
                OrderId = 3,
                TotalPrice = (decimal)299.99,
                OrderDate = date
            };

            Assert.Equal(1, order.CustomerId);
            Assert.Equal(2, order.StoreId);
            Assert.Equal(3, order.OrderId);
            Assert.Equal((decimal)299.99, order.TotalPrice);
            Assert.Equal(date, order.OrderDate);
        }

        [Fact]
        public void OrderItemTest()
        {
            d.OrderItem order = new d.OrderItem
            {
                OrderItemId = 1,
                OrderId = 3,
                Amount = 6,
                ProducutName = "NS"
            };

            Assert.Equal(1, order.OrderItemId);
            Assert.Equal(3, order.OrderId);
            Assert.Equal(6, order.Amount);
            Assert.Equal("NS", order.ProducutName);
        }

        [Fact]
        public void ProductTest()
        {
            d.Product product = new d.Product
            {
                ProductId = 1,
                ProductName = "NS",
                UnitPrice = (decimal)299.99
            };

            Assert.Equal(1, product.ProductId);
            Assert.Equal("NS", product.ProductName);
            Assert.Equal((decimal)299.99, product.UnitPrice);
        }

        [Fact]
        public void StoreTest()
        {
            d.Store store = new d.Store
            {
                StoreId = 1,
                StoreName = "GCS",
                City = "AR",
                Postal = "76010"
            };

            Assert.Equal(1, store.StoreId);
            Assert.Equal("GCS", store.StoreName);
            Assert.Equal("AR", store.City);
            Assert.Equal("76010", store.Postal);
        }
    }
}
