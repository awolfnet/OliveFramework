using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.model
{
    public class MessageModel
    {
        public enum MESSAGE_TYPE
        {
            MAIL = 0,
            ALERT,
        };

        public enum MESSAGE_STYLE
        {
            CRITICAL = 0,
            QUESTION,
            EXCLAMATION,
            INFORMATION,


        };

        public MESSAGE_TYPE MessageType;
        public MESSAGE_STYLE MessageStyle;
        public int MessageID;
        public string MessageTitle;
        public string MessageContent;
    }
}