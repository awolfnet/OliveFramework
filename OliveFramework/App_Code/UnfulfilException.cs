using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework
{
    public class UnfulfilException:Exception
    {
        public int Code;
        public string DisplayMessage;

        public UnfulfilException(int ErrorCode, string DisplayMessage)
        {
            this.Code = ErrorCode;
            this.DisplayMessage = DisplayMessage;
        }

        public UnfulfilException(string LangPath)
        {
            int errorCode = Lang.getCode(LangPath);
            string errorMessage = Lang.get(LangPath);

            this.Code = errorCode;
            this.DisplayMessage = errorMessage;
        }
    }
}