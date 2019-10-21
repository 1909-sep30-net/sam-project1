using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GStoreApp.Library;

namespace GStore.WebUI.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<string> ProductName { get; set; }
        public List<int> Amount { get; set; }

    }
}
