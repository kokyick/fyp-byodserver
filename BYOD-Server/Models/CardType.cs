using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class CardType
    {
        [Key]
        public int Card_type_id { get; set; }
        public string Name { get; set; }
        public ICollection<StripeCards> StripeCards { get; set; }

    }
}