using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GStoreApp.Library;

namespace GStore.WebUI.Models
{
    public class Receipt
    {
        public Order Order { get; set; }
        public int orderId { get; set; }
    }
}
