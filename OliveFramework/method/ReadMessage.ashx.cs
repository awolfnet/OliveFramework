using OliveFramework.Model.Request;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// ReadMessage 的摘要说明
    /// </summary>
    public class ReadMessage : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            ReachedMessageList reachedMessageList = getModel<ReachedMessageList>();

            try
            {
                if(reachedMessageList==null || reachedMessageList.Count<=0)
                {
                    throw new ExceptionMessage("/language/parameter/lack");
                }

                foreach(uint messageID in reachedMessageList)
                {
                    new Controller.Message().MakeMessageRead(messageID);
                }

                WriteSuccess<String>("success");
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