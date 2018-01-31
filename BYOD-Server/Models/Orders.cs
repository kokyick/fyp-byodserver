using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; }
        public DateTime order_time { get; set; }
        public decimal total_bill { get; set; }
        public string comments { get; set; }
        public bool completed { get; set; }
        public bool paid { get; set; }
        public bool recently_changed { get; set; }
        public int? promocode_id { get; set; }
        [ForeignKey("promocode_id")]
        public Promocodes promocod { get; set; }
        public int? payment_id { get; set; }
        [ForeignKey("payment_id")]
        public PaymentHistory paymentHistory { get; set; }
        public int? table_id { get; set; }
        [ForeignKey("table_id")]
        public RestaurantTable restaurantTable { get; set; }

        public string user_id { get; set; }
        [ForeignKey("user_id")]
        public ApplicationUser User { get; set; }

        public ICollection<FoodOrdered> foodOrdered { get; set; }
    }
}