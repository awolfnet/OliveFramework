using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// PullMessage 的摘要说明
    /// </summary>
    public class PullMessage : BasePage
    {
        private DateTime _startTime ;
        protected override void OnRequest()
        {
            base.OnRequest();

            int PullTimeout = SystemConfig.MessagePullingTimeout;
            int PullSleep = SystemConfig.MessagePullingSleep;

            Server.ScriptTimeout = PullTimeout+10;
            _startTime = DateTime.Now;

            ReachedMessageList reachedMessageList = getModel<ReachedMessageList>();
            
            while (_startTime.AddSeconds(PullTimeout)>= DateTime.Now)
            {
                if (!Response.IsClientConnected)
                    WriteEnd();

                
                MessageListModel messageList = GetUnreachedMessage(reachedMessageList);
                if(messageList.Count>0)
                {
                    WriteSuccess<MessageListModel>(messageList);
                    WriteEnd();
                }

                Thread.Sleep(PullSleep);
            }

            WriteUnfulfil("No message");
        }
        
        private MessageListModel GetUnreachedMessage(ReachedMessageList reachedMessageList)
        {

            MessageListModel messageList = new MessageListModel();

            using (Controller.Message controllerMessage = new Controller.Message())
            {

                List<MessageModel> recordList = controllerMessage.GetUserMessage(aid, Controller.Message.MESSAGE_STATUS.UNREACH);

                if(recordList!=null)
                {
                    foreach (MessageModel message in recordList)
                    {
                        if (reachedMessageList != null)
                        {
                            if (reachedMessageList.Contains((uint)message.mid))
                            {
                                continue;
                            }
                        }
                        messageList.Add(message);
                    }
                }

            }

            return messageList;
        }

    }
}