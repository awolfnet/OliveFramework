using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetSystemConfig 的摘要说明
    /// </summary>
    public class GetSystemConfig : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            SystemConfigModel sysConfig = new SystemConfigModel();

            sysConfig.WindowTitle = OliveFramework.SystemConfig.WindowTitle;
            sysConfig.BannerTitle = "Dashboard";
            WriteSuccess<SystemConfigModel>(sysConfig);
        }

    }
}