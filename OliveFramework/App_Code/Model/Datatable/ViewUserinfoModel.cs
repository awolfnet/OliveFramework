using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class ViewUserinfoModel
    {
        public uint? aid { set; get; }
        public String Name { set; get; }
        public String Mail { set; get; }
        public uint? pid { set; get; }
        public uint? gid { set; get; }
        public String Nickname { set; get; }
        public String Avatar { set; get; }
        public DateTime MemberValid { set; get; }
    }
}