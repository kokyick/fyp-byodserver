using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class ReviewRatings
    {
        [Key]
        public int Review_Ratings_id { get; set; }
        public DateTime? review_time { get; set; }
        public decimal ratings { get; set; }
        public string comments { get; set; }

        public int outlet_id { get; set; }
        [ForeignKey("outlet_id")]
        public Outlets outlet { get; set; }
        public string user_id { get; set; }
        [ForeignKey("user_id")]
        public ApplicationUser user { get; set; }
    }
}