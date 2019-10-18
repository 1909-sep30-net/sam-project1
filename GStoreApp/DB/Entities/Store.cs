using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Store
    {
        public Store()
        {
            Customer = new HashSet<Customer>();
            Inventory = new HashSet<Inventory>();
            OrderOverView = new HashSet<OrderOverView>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderOverView> OrderOverView { get; set; }
    }
}
