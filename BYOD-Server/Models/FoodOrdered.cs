using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class FoodOrdered
    {
        [Key]
        public int food_ordered_id { get; set; }
        public bool served { get; set; }
        public int quantity { get; set; }
        public string comments { get; set; }
        public bool newly_added { get; set; }

        public int outlet_product_id { get; set; }
        [ForeignKey("outlet_product_id")]
        public OutletProduct outletProduct { get; set; }
        public int order_id { get; set; }
        [ForeignKey("order_id")]
        public Orders Order { get; set; }
    }
}