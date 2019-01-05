using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// DeleteAd 的摘要说明
    /// </summary>
    public class DeleteAd : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                List<uint> adList = getModel<List<uint>>("ad_list");

                using (Controller.Ad controllerAd = new Controller.Ad())
                {
                    foreach (uint adID in adList)
                    {
                        controllerAd.DeleteAd(adID);
                    }
                }

                WriteSuccess<string>("success");

            }
            catch (UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }
            catch (Database.Exception ex)
            {
                WriteException(ex);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
    }
}