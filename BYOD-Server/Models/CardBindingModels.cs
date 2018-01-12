using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BYOD_Server.Models
{
    public class CardBindingModels
    {
        public class CardPayment
        {
            public int Card_id { get; set; }
            public string User_id { get; set; }
            public string Payment_key { get; set; }
            public int Order_ID { get; set; }

        }
    }
}