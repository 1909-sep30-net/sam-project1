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

        [RegularExpression(@"^[a-zA-Z""'\s-]*$",ErrorMessage = "The Input Must be letters")
            , Required, StringLength(15)]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "The Input Must be letters")
            , Required, StringLength(15)]
        public string LastName { get; set; }

        [RegularExpression(@"[0-9""'\s-]*$", ErrorMessage = "The input must be 10 numbers")
            , Required, StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; }

        [Required, Range(1,3, ErrorMessage = "The number must between 1 to 3")]
        public int FavoriteStore { get; set; }

    }
}
