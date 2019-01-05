using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetChatHistory 的摘要说明
    /// </summary>
    public class GetChatHistory : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();
            uint? talkerID = getParameterUint("talker_id");

            //MessageListModel

            try
            {
                if(talkerID==null)
                {
//                    throw new UnfulfilException();
                }

                MessageListModel chatHistoryList= GetChatMessageHistory(aid,(uint)talkerID);

                WriteSuccess<MessageListModel>(chatHistoryList);
            }
            catch (UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }
            catch(Database.Exception ex)
            {
                WriteException(ex);
            }catch(Exception ex)
            {
                WriteException(ex);
            }

        }

        private MessageListModel GetChatMessageHistory(uint UserID,uint TalkerID)
        {
            MessageListModel chatHistoryList = new MessageListModel();
            List<MessageModel> messageList = new Controller.Message().GetMessageWithUser(UserID,TalkerID);
            if(messageList!=null)
            {
                foreach(MessageModel message in messageList)
                {
                    chatHistoryList.Add(message);
                }
            }
            return chatHistoryList;
        }
    }
}