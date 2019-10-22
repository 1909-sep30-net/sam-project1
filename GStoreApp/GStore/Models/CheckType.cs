using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GStore.WebUI.Models
{
    public class CheckType
    {
        [Range(0,999999)]
        public int OrderId { get; set; }
        [Range(0, 999999)]
        public int StoreId { get; set; }
        [Range(0, 999999)]
        public int CustomerId { get; set; }
    }
}
