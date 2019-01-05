using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class CargoOrderModel
    {
        public uint? id { set; get; }
        public String orderid { set; get; }
        public uint? cargoid { set; get; }
        public uint? buyerid { set; get; }
        public DateTime orderdate { set; get; }
        public String comment { set; get; }
        public decimal amount { set; get; }

    }
}