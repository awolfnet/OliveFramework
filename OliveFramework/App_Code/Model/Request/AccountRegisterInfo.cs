﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Request
{
    public class AccountRegisterInfo
    {
        public String account_name { set; get; }
        public String account_password { set; get; }
        public String account_mail { set; get; }
        public String account_nickname { set; get; }

    }
}