using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework
{
    public class ExceptionMessage:Exception
    {
        public int Code;
        public string DisplayMessage;

        public ExceptionMessage(int ErrorCode,string DisplayMessage)
        {
            this.Code = ErrorCode;
            this.DisplayMessage = DisplayMessage;
        }
       
        public ExceptionMessage(string LangPath)
        {
            int errorCode = Lang.getCode(LangPath);
            string errorMessage = Lang.get(LangPath);

            this.Code = errorCode;
            this.DisplayMessage = errorMessage;
        }
    }
}