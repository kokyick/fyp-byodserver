using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class Outlets
    {
        [Key]
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
        [ForeignKey("merchant_id")]
        public Merchants merchant { get; set; }
        public ICollection<ReviewRatings> ReviewNRatings { get; set; }
        public ICollection<OutletProduct> OutletProduct { get; set; }
        public ICollection<RestaurantTable> RestaurantTable { get; set; }

    }
}