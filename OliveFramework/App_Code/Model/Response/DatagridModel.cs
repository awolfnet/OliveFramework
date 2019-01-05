using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Response
{
    public class DatagridModel<T>
    {
        public uint total { set; get; }
        public List<T> rows { set; get; }

        public DatagridModel()
        {
            //this.rows = new List<T>();
        }
    }
}