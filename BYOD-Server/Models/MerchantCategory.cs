using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class MerchantCategory
    {
        [Key]
        public int merchantCat_id { get; set; }
        public string name { get; set; } //western, etc.

        public ICollection<Merchants> merchant { get; set; }
    }
}