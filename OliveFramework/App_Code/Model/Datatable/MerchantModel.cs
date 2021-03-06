﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class MerchantModel
    {
        public uint? merchantid { set; get; }
        public string name { set; get; }
        public string contact { set; get; }
        public string website { set; get; }
        public uint? marketid { set; get; }
        public string description { set; get; }
        public uint? aid { set; get; }
        public int? mtypeid { set; get; }
    }
}