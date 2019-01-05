using OliveFramework.Model.Request;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// SendChat 的摘要说明
    /// </summary>
    public class SendChat : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            ChatContent chatContent = getModel<ChatContent>();

            try
            {
                if (chatContent != null)
                {
                    UserinfoModel userModel = null;
                    using (Controller.Account controllerAccount = new Controller.Account())
                    {
                        userModel = controllerAccount.GetUserinfo(aid);
                        if (userModel == null)
                        {
                            throw new UnfulfilException(0, "未找到指定用户");
                        }
                    }

                    String sendUserName = userModel.userNickname;
                    new Controller.Message().AddMessage(chatContent.userid, chatContent.content, this.aid, Controller.Message.MESSAGE_STATUS.UNREACH, Controller.Message.MESSAGE_STYLE.INFORMATION, sendUserName, Controller.Message.MESSAGE_TYPE.CHAT);
                    WriteSuccess<string>("发送成功");
                }else
                {
                    WriteUnfulfil("");
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
            

        }

    }
}