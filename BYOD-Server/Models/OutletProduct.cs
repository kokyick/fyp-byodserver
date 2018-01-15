using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class OutletProduct
    {
        [Key]
        public int outlet_product_id { get; set; }
        public bool out_of_stock { get; set; }
        public int? merchant_product_id { get; set; }
        [ForeignKey("merchant_product_id")]
        public MerchantProduct merchantProduct { get; set; }
        public int? outlet_id { get; set; }
        [ForeignKey("outlet_id")]
        public Outlets outlet { get; set; }
        public ICollection<FoodOrdered> foodOrdered { get; set; }
    }
}