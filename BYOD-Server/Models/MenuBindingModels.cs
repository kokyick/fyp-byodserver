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
    }
}