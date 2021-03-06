﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model
{
    public class ViewOrderHistoryListModel
    {
        public uint? id { set; get; }
        public string order_id { set; get; }
        public DateTime? order_date { set; get; }
        public string order_list { set; get; }
        public string order_comment { set; get; }
        public decimal? order_amount { set; get; }
        public string merchant_name { set; get; }
        public string merchant_contact { set; get; }
        public uint? merchant_id { set; get; }
        public uint? merchant_owner_id { set; get; }
        public string buyer_contact { set; get; }
        public uint? buyer_id { set; get; }
        public string buyer_name { set; get; }
    }
}