using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class Merchants
    {
        [Key]
        public int merchant_id { get; set; }
        public string merchant_photo { get; set; }
        public string biz_reg_no { get; set; }
        public bool biz_verified { get; set; }
        public string biz_add { get; set; }
        public string biz_postal { get; set; }
        public string biz_name { get; set; }
        public string office_no { get; set; }
        public string mobile_no { get; set; }

        public int merchant_cat { get; set; }
        [ForeignKey("merchant_cat")]
        public MerchantCategory merchantCategory { get; set; }

        public string user_id { get; set; }
        [ForeignKey("user_id")]
        public ApplicationUser user { get; set; }

        public ICollection<Outlets> outlets { get; set; }
        public ICollection<MerchantProduct> merchantProduct { get; set; }
    }
}