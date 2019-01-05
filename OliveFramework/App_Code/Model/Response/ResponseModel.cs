using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.Model.Response
{
    public enum RESPONSE_CODE
    {
        SUCCESS = 1,
        FAIL,
        UNFULFIL,
        EXCEPTION,
    };

    public class ResponseModel<T>
    {

        public bool success;
        public RESPONSE_CODE code;
        public T message;

        public ResponseModel(T t, bool success, RESPONSE_CODE code)
        {
            this.message = t;
            this.success = success;
            this.code = code;
        }

    }
}