using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class PaymentHistory
    {
        [Key]
        public int paymentHistory_id { get; set; }
        public DateTime? payment_time { get; set; }
        public decimal amouunt { get; set; }
        public string remarks { get; set; }

        public string user_id { get; set; }
        [ForeignKey("user_id")]
        public ApplicationUser owner { get; set; }
        public int card_id { get; set; }
        [ForeignKey("card_id")]
        public StripeCards card { get; set; }

        public ICollection<Orders> orders { get; set; }

    }
}