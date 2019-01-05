using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// AddAd 的摘要说明
    /// </summary>
    public class AddAd : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                string imgSrc = getParameter("img_src");
                string imgLink = getParameter("img_link");
                uint pageLevel = (uint)getParameterUint("page_level");

                using (Controller.Ad controllerAd = new Controller.Ad())
                {
                    controllerAd.AddAd(imgSrc,imgLink,null, pageLevel);
                }

                WriteSuccess<String>("success");
            }
            catch(ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.DisplayMessage);
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