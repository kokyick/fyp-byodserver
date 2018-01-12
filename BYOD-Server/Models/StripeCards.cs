using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class StripeCards
    {
        [Key]
        public int Card_id { get; set; }
        public string Last_four_digit { get; set; }
        public string Payment_key { get; set; }

        public string User_id { get; set; }
        [ForeignKey("User_id")]
        public ApplicationUser Owner { get; set; }
        public int Card_type_id { get; set; }
        [ForeignKey("Card_type_id")]
        public CardType CardType { get; set; }

        public ICollection<PaymentHistory> PaymentHistory { get; set; }
    }
}