﻿using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetSessionList 的摘要说明
    /// </summary>
    public class GetSessionList : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            object o=new Controller.Session().ListAllSession();

            WriteSuccess<object>(o);
        }

    }
}