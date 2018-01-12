using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class OutletReview
    {
        [Key]
        public int Oreview_id { get; set; }


        public int outlet_id { get; set; }
        [ForeignKey("outlet_id")]
        public Outlets outlet { get; set; }

        
    }
}