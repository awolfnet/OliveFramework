using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetMyip 的摘要说明
    /// </summary>
    public class GetMyip : BasePage
    {
        protected override void OnRequest()
        {
            Myip myip = new Myip();
            
            myip.ipaddress = Request.ServerVariables["REMOTE_HOST"];
            myip.port = Int32.Parse(Request.ServerVariables["REMOTE_PORT"]);
            myip.useragent = Request.ServerVariables["HTTP_USER_AGENT"];
            myip.servertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            WriteSuccess<Myip>(myip);
        }

        private class Myip
        {
            public string ipaddress;
            public int port;
            public string useragent;
            public string servertime;
        }
    }
}