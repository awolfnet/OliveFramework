using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework
{
    /// <summary>
    /// qr 的摘要说明
    /// </summary>
    public class qr : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect("weixin://qr/Eb1xaaTEFflqreAK9_gD");

            //context.Response.ContentType = "text/plain";

            //context.Server.Transfer("http://weixin.qq.com/r/xrXK0qfEvvrBreOx9_Cg");

            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}