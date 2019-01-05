using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.model
{
    public class UserLoginModel
    {
        public int UserID;
        public string Username;
        public string Token;
        public string Expire;
        public string Redirect;
    }

}