﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class ViewMarketListModel
    {
        public uint? market_id { set; get; }
        public string market_name { set; get; }
        public string market_location { set; get; }
        public string market_typename { set; get; }

    }
}