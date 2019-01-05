using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace OliveFramework.webservice
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class User : System.Web.Services.WebService
    {

        [WebMethod]
        public string Login(string username,string password)
        {
            return "Hello " + username;
        }
    }
}
