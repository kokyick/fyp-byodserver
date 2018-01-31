using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class MenuBindingModels
    {
        public class Foodserved
        {
            public int fdId { get; set; }
            public bool served { get; set; }
        }
        public class TableUpdate
        {
            public int table_id { get; set; }
            public int order_id { get; set; }
        }
        public class GetOutletMenu
        {
            public int merchant_product_id { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
            public string product_image { get; set; }
            public decimal avg_ratings { get; set; }
            public int food_type_id { get; set; }
            public int? outlet_id { get; set; }
            public bool outofstock { get; set; }
            public int outletproduct_id { get; set; }
        }
        public class GetOrderedItems
        {
            public int merchant_product_id { get; set; }
            public DateTime? order_time { get; set; }
            public string name { get; set; }
            public int quantity { get; set; }
            public string product_image { get; set; }
            public decimal price { get; set; }
            public int merchant_id { get; set; }
            public int order_id { get; set; }
            public decimal order_bill { get; set; }
            public bool order_status { get; set; }
            public bool dish_completed { get; set; }
            public int food_order_id { get; set; }
            public int? table_id { get; set; }
            public string order_comment { get; set; }
            public string food_comments { get; set; }
        }
        public class OpenCloseOrder
        {
            public int merchant_product_id { get; set; }
            public DateTime? order_time { get; set; }
            public string name { get; set; }
            public int quantity { get; set; }
            public string product_image { get; set; }
            public decimal price { get; set; }
            public int merchant_id { get; set; }
            public int order_id { get; set; }
            public decimal order_bill { get; set; }
            public bool order_status { get; set; }
            public bool dish_completed { get; set; }
            public int food_order_id { get; set; }
            public int? table_id { get; set; }
            public string order_comment { get; set; }
            public string food_comments { get; set; }
            public string merchant_name { get; set; }
            public string promocode_name { get; set; }
            public DateTime? promo_start_date { get; set; }
            public DateTime? promo_expire_date { get; set; }
            public decimal? discount { get; set; }
            public decimal gst { get; set; }
            public decimal svscharge { get; set; }
        }
        public class FoodServed
        {
            public int food_ordered_id { get; set; }
            public int order_id { get; set; }
        }

        public class GetOrder
        {
            public int order_id { get; set; }
            public decimal order_bill { get; set; }
            public DateTime? order_time { get; set; }
            public bool order_status { get; set; }
            public bool order_payment { get; set; }
            public int outlet_id { get; set; }
            public string outlet_name { get; set; }

        }
        public class GetReport
        {
            public int order_id { get; set; }
            public decimal order_bill { get; set; }
            public DateTime? order_time { get; set; }
            public bool order_status { get; set; }
            public bool order_payment { get; set; }
            public int outlet_id { get; set; }
            public string outlet_name { get; set; }
            public int merchant_id { get; set; }
            public string merchantname { get; set; }
            public string merchantdp { get; set; }

        }
        public class PostGetReport
        {
            public string startdate { get; set; }
            public string enddate { get; set; }
        }
        public class CalculateBill
        {
            public int order_id { get; set; }
            public decimal order_bill { get; set; }
            public decimal outlet_sc { get; set; }
            public decimal outlet_gst { get; set; }
            public decimal mp_price { get; set; }
            public int food_quantity { get; set; }
        }
        public class OutOfStock
        {
            public int OP_ID { get; set; }
            public int stockstatus { get; set; }
        }
        public class MOutlet
        {
            public int outlet_id { get; set; }
            public string featured_photo { get; set; }
            public string name { get; set; }
            public string streetname { get; set; }
            public string unit_no { get; set; }
            public string postal_code { get; set; }
            public string contact_no { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public DateTime? opening_time { get; set; }
            public DateTime? closing_time { get; set; }
            public bool opening_status { get; set; }
            public decimal gst { get; set; }
            public decimal servicecharge { get; set; }

            //reviews & ratings
            public DateTime? last_review_time { get; set; }
            public decimal avg_ratings { get; set; }
            public int total_comments { get; set; }

            public int merchant_id { get; set; }
        }
        public class MProduct
        {
            public int merchant_product_id { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
            public string product_image { get; set; }
            public decimal avg_ratings { get; set; }
            public bool deleted { get; set; }

            public int food_type { get; set; }

            public int merchant_id { get; set; }
        }

        internal class NewlyAdded
        {
            internal int dish_id;

            public bool newly_added { get; set; }
        }
    }
}