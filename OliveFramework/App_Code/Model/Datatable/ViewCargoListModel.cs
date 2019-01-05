using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class ViewCargoListModel
    {
        public uint? cargo_id { set; get; }
        public uint? cargo_owner_id { set; get; }
        public string cargo_plate { set; get; }
        public string cargo_driver { set; get; }
        public string driver_licence { set; get; }
        public string driver_contact { set; get; }
        public string cargo_description { set; get; }
        public string market_name { set; get; }

    }
}