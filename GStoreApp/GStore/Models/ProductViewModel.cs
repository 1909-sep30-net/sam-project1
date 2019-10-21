using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GStore.WebUI.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
