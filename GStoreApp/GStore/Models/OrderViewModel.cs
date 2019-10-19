using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GStore.WebUI.Models
{
    public class OrderViewModel
    {
        [DisplayName("Nintendo Switch"), Range(0, 10)]
        public int NSAmount { get; set; }

        [DisplayName("Xbox One"), Range(0, 10)]
        public int XBOAmount { get; set; }

        [DisplayName("Playstation 4 Pro"), Range(0, 10)]
        public int PS4PAmount { get; set; }
        [DisplayName("Playstation"), Range(0, 10)]
        public int PS4Amount { get; set; }

        [DisplayName("Xbox 360"), Range(0, 10)]
        public int XB360Amount { get; set; }

        [DisplayName("Playstation 3"), Range(0, 10)]
        public int PS3Amount { get; set; }

    }
}
