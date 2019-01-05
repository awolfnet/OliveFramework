using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework
{
    /// <summary>
    /// t 的摘要说明
    /// </summary>
    public class t : BasePage
    {

        protected override void OnRequest()
        {
            WriteJson(Request.UrlReferrer);
        }

    }
}