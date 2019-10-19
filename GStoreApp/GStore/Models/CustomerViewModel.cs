using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GStore.WebUI.Models
{
    public class CustomerViewModel
    {
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [RegularExpression(@"^[a-zA-Z""'\s-]*$"), Required, StringLength(15)]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z""'\s-]*$"), Required, StringLength(14)]
        public string LastName { get; set; }

        [RegularExpression(@"[0-9""'\s-]*$"), Required, StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; }

        [Required]
        public int FavoriteStore { get; set; }

    }
}
