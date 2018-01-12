using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class MerchantProduct
    {
        [Key]
        public int merchant_product_id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string product_image { get; set; }
        public decimal avg_ratings { get; set; }
        public bool deleted { get; set; }

        public int food_type { get; set; }
        [ForeignKey("food_type")]
        public FoodType foodType { get; set; }

        public int merchant_id { get; set; }
        [ForeignKey("merchant_id")]
        public Merchants merchant { get; set; }

        public ICollection<OutletProduct> outletproduct { get; set; }

    }
}