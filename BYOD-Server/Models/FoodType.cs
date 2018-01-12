using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class FoodType
    {
        [Key]
        public int type_id { get; set; }
        public string name { get; set; } //halal, vegan, etc.

        public ICollection<MerchantProduct> merchantProduct { get; set; }
    }
}