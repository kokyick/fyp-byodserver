using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class Promocodes
    {
        [Key]
        public int promocodes_id { get; set; }
        public string promocode_name { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? expire_date { get; set; }
        public decimal? discount { get; set; }
        public ICollection<Orders> orders { get; set; }
    }
}