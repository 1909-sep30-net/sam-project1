using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GStore.WebUI.Models
{
    public class DisplayOrder
    {
        public List<int> OrderId { get; set; }
        public List<int> CustomerId { get; set; }
        public List<int> StoreId { get; set; }
        public List<DateTime> OrderDate { get; set; }
        public List<decimal> TotalPrice { get; set; }
    }
}
