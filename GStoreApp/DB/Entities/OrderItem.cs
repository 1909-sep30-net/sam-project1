using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ProducutName { get; set; }
        public int Amount { get; set; }

        public virtual OrderOverView Order { get; set; }
        public virtual Product ProducutNameNavigation { get; set; }
    }
}
