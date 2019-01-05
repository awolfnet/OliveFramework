using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace OliveFramework.tool
{
    public class WebConfig
    {
        public static string get(string configKey)
        {
                return ConfigurationManager.AppSettings[configKey];
        }

        public static bool isset(string configKey)
        {
            if (ConfigurationManager.AppSettings[configKey] != null)
                return true;
            else
                return false;
        }


    }
}