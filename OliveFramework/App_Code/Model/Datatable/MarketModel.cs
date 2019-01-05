using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class MarketModel
    {
        public uint? marketid { set; get; }
        public string name { set; get; }
        public string location { set; get; }
        public uint? markettypeid { set; get; }

    }
}