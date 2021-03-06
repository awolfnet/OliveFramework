﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class ViewMerchantFavoriteListModel
    {
        public uint? favorite_id { set; get; }
        public uint? merchant_id { set; get; }
        public string merchant_name { set; get; }
        public string merchant_contact { set; get; }
        public string merchant_website { set; get; }
        public string merchant_description { set; get; }
        public uint? merchant_owner_id { set; get; }
        public uint? aid { set; get; }
    }
}