﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class ProfileModel
    {
        public uint? profileid { set; get; }
        public String Nickname { set; get; }
        public String Avatar { set; get; }
        public DateTime? MemberValid { set; get; }

    }
}