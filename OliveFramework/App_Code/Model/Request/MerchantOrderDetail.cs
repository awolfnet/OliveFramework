﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Request
{
    public class MerchantOrderDetail
    {
        public uint? merchantid { set; get; }
        public uint? buyerid { set; get; }
        public DateTime orderdate { set; get; }
        public string orderlist { set; get; }
        public string comment { set; get; }
        public decimal amount { set; get; }
        public int amounttype { set; get; }

    }
}