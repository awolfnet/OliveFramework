using System;
using System.Web;

namespace OliveFramework.js
{
    /// <summary>
    /// jsParameters 的摘要说明
    /// </summary>
    public class jParameters : IHttpHandler
    {
        HttpContext Context = null;
        public void ProcessRequest(HttpContext context)
        {
            this.Context = context;
            Context.Response.ContentType = "application/javascript";
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void exportVar(string varName,string varValue)
        {
            string js = "var {0}=\"{1}\";";
            Context.Response.Write(String.Format(js,varName,varValue));
        }

        private void exportVar(string varName, int varValue)
        {
            string js = "var {0}={1};";
            Context.Response.Write(String.Format(js, varName, varValue));
        }
    }
}