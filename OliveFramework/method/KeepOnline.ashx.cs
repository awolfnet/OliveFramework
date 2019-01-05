using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// KeepOnline 的摘要说明
    /// </summary>
    public class KeepOnline : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                new Controller.Session().Keepalive(Token);

                WriteSuccess<string>("success");
            }
            catch (ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.DisplayMessage);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }

            
            
        }

    }
}