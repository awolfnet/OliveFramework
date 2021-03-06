﻿using OliveFramework.Model.Datatable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Message: BaseController
    {
        public enum MESSAGE_STATUS
        {
            UNREACH = 0,
            UNREAD,
            REACH,
            READ,
        };

        public enum MESSAGE_TYPE
        {
            CONFIG = 0,
            MAIL,
            ALERT,
            EVENT,
            CHAT,
        };

        public enum MESSAGE_STYLE
        {
            CRITICAL = 0,
            QUESTION,
            EXCLAMATION,
            INFORMATION,
            
        };
        public enum CHAT_TYPE
        {
            TEXT,
            IMAGE,
            VOICE,
        }

        /// <summary>
        /// 获取用户消息
        /// </summary>
        /// <param name="aid">用户ID</param>
        /// <param name="MessageStatus">消息状态</param>
        /// <returns></returns>
        public List<MessageModel> GetUserMessage(uint aid, MESSAGE_STATUS MessageStatus)
        {
            string where = string.Format("Message.Status={0} AND Message.aid={1}", (int)MessageStatus,(uint)aid);
            return db.SelectData<MessageModel>("Message", where);
        }

        public List<MessageModel> GetUserMessage(uint aid, MESSAGE_STATUS? MessageStatus,MESSAGE_STYLE? MessageStyle,MESSAGE_TYPE? MessageType)
        {
            string where = string.Format("Message.aid={0}", (uint)aid);

            if(MessageStatus!=null)
            {
                where += string.Format(" AND Message.Status={0}", (int)MessageStatus);
            }

            if(MessageStyle!=null)
            {
                where += string.Format(" AND Message.Style={0}", (int)MessageStyle);
            }

            if (MessageType != null)
            {
                where += string.Format(" AND Message.Type={0}", (int)MessageType);
            }

            return db.SelectData<MessageModel>("Message", where);
        }

        /// <summary>
        /// 获取用户消息
        /// </summary>
        /// <param name="aid">用户ID</param>
        /// <returns></returns>
        public List<MessageModel> GetUserMessage(uint aid)
        {
            string where = string.Format("Message.aid={0}", aid);
            return db.SelectData<MessageModel>("Message", where);
        }

        /// <summary>
        /// 标记消息为已读
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        public void MakeMessageRead(uint MessageID)
        {
            UpdateMessageStatus(MessageID, MESSAGE_STATUS.READ);
        }

        public void AddMessage(uint? AccountID,string Content,uint? SourceID,Message.MESSAGE_STATUS Status,Message.MESSAGE_STYLE Style,String Title,Message.MESSAGE_TYPE Type)
        {
            MessageModel message = new MessageModel();

            message.aid = AccountID;
            message.Content = Content;
            message.fromid = SourceID;
            message.Status =(uint)Status;
            message.Style =(uint)Style;
            message.Title = Title;
            message.Type = (uint)Type;
            message.timestamps = DateTime.Now;

            db.InsertSingleLine<MessageModel>("Message", message);
        }

        public List<MessageModel> GetMessageWithUser(uint UserID,uint TalkerID)
        {
            string where = string.Format("(Message.aid={0} AND Message.fromid={1}) OR (Message.aid={1} AND Message.fromid={0}) AND Message.Type=4 ORDER BY Message.timestamps ASC", (uint)UserID,(uint)TalkerID);

            return db.SelectData<MessageModel>("Message",where);

        }

        private void UpdateMessageStatus(uint MessageID, MESSAGE_STATUS MessageStatus)
        {
            string where = string.Format("Message.mid={0}", MessageID);
            MessageModel message = new MessageModel();
            message.aid = null;
            message.Content = null;
            message.fromid = null;
            message.mid = null;
            message.Style = null;
            message.Title = null;
            message.Type = null;
            message.Status =(uint)MessageStatus;
            message.timestamps = null;
            db.UpdateSingleLine<MessageModel>("Message", message, where);
        }

        public void PushMessage(String Title,String Content, Message.MESSAGE_TYPE Type, Message.MESSAGE_STYLE Style)
        {
            Hashtable ht = new Hashtable();

            ht.Add("Title", Title);
            ht.Add("Content", Content);
            ht.Add("Type", Type);
            ht.Add("Style", Style);

            db.CallProcedure("PushMessage", ht);
        }
    }
}