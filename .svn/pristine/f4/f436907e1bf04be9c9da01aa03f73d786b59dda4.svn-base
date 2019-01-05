using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetAdList 的摘要说明
    /// </summary>
    public class GetAdList : BasePage
    {
        //

        protected override void OnRequest()
        {
            try
            {
                uint? pageLevel = getParameterUint("pagelevel");

                AdListModel adList = null;

                using (Controller.Ad controllerAd = new Controller.Ad())
                {
                    adList = controllerAd.GetAdList(pageLevel);
                }

                //List<Dictionary<string, string>> adList = Controller.Ad.Instance.GetAdList(null);
                WriteSuccess<AdListModel>(adList);
                
            }catch(Exception ex)
            {
                WriteException(ex);
            }

            WriteEnd();
        }

    }
}