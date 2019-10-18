using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            OrderOverView = new HashSet<OrderOverView>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? FavoriteStore { get; set; }

        public virtual Store FavoriteStoreNavigation { get; set; }
        public virtual ICollection<OrderOverView> OrderOverView { get; set; }
    }
}
