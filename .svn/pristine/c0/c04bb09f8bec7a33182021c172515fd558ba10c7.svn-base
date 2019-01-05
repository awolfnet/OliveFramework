using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// PushMessage 的摘要说明
    /// </summary>
    public class PushMessage : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            Model.Request.PushMessage message = new Model.Request.PushMessage();

            try
            {
                message = getModel<Model.Request.PushMessage>();

                using (Controller.Message controllerMessage = new Controller.Message())
                {
                    controllerMessage.PushMessage(message.Title, message.Content, Controller.Message.MESSAGE_TYPE.MAIL, Controller.Message.MESSAGE_STYLE.INFORMATION);
                }

                WriteSuccess<String>("推送成功");
            }catch(Exception ex)
            {
                WriteException(ex);
            }

        }
    }
}