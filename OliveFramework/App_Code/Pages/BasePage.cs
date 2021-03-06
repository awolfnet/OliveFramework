﻿using Newtonsoft.Json;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using OliveFramework.tool;
using OliveFramework.Model.Datatable;

namespace OliveFramework.Page
{
    public abstract class BasePage : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        protected HttpRequest Request;
        protected HttpResponse Response;
        protected HttpServerUtility Server;

        private string _token = "";
        protected string Token
        {
            set            {            }
            get
            {
                if(string.IsNullOrWhiteSpace(_token))
                {
                    _token = getParameter("token");
                }
                return _token;
            }
        }

        protected uint? page
        {
            get
            {
                return getParameterUint("page");
            }
        }
        protected uint? rows
        {
            get
            {
                return getParameterUint("rows");
            }
        }

        private ViewSessionUserModel _sessionUser = null;

        private Controller.Session.SESSION_STATUS _userSessionStatus = Controller.Session.SESSION_STATUS.UNKNOW;
        protected Controller.Session.SESSION_STATUS UserSessionStatus
        {
            set { }
            get
            {
                if (_userSessionStatus == Controller.Session.SESSION_STATUS.UNKNOW)
                {
                    if (_sessionUser == null)
                    {
                        _userSessionStatus = Controller.Session.SESSION_STATUS.NOTEXIST;
                    }else
                    {
                        if (DateTime.Compare((DateTime)_sessionUser.ValidDay, DateTime.Now) > 0)
                        {
                            _userSessionStatus = Controller.Session.SESSION_STATUS.AVAILABLE;
                        }
                        else
                        {
                            _userSessionStatus = Controller.Session.SESSION_STATUS.EXPIRED;
                        }
                    }
                }
                
                return (Controller.Session.SESSION_STATUS)_userSessionStatus;
            }
        }

        private System.Text.Encoding _pageEncoding=null;
        protected System.Text.Encoding PageEncoding
        {
            get
            {
                if(_pageEncoding == null)
                {
                    return System.Text.Encoding.UTF8;
                }else
                {
                    return _pageEncoding;
                }
            }
            set
            {
                _pageEncoding = value;
            }
        }

        protected string PageFile
        {
            set
            {
            }
            get
            {
                return System.IO.Path.GetFileName(Request.Path).ToString();
            }

        }

        private uint? _privilegeID = null;
        protected uint PrivilegeID
        {
            set
            {

            }
            get
            {

                if (_privilegeID != null)
                {
                    return (uint)_privilegeID;
                }

                if (_sessionUser == null)
                {
                    _privilegeID = 1;
                }
                else
                {
                    _privilegeID = _sessionUser.pid;
                }
                

                return (uint)_privilegeID;
            }
        }

        private uint? _aid = null;
        protected uint aid
        {
            set
            {

            }
            get
            {
                if(_aid != null)
                {
                    return (uint)_aid;
                }

                if (_sessionUser == null)
                {
                    _aid = 0;
                }else
                {
                    _aid = _sessionUser.aid;
                }

                return (uint)_aid;
            }
        }

        protected bool MemberValid
        {
            set           {            }
            get
            {
                if(_sessionUser.MemberValid==null)
                {
                    return false;
                }

                if (DateTime.Compare((DateTime)_sessionUser.MemberValid, DateTime.Now) > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }


        public void ProcessRequest(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Server = context.Server;

            if (Request.Browser.Browser == "InternetExplorer")
            {
                Response.ContentType = "text/json";
            }else
            {
                Response.ContentType = "application/json";
            }

            this.OnRequest();

            Response.Charset = getCharset();
        }

        protected virtual void OnRequest()
        {
            if(string.IsNullOrWhiteSpace(Token))
            {
                WriteFail(tool.Lang.get("/language/session/no_session"));
                WriteEnd();
            }

            _sessionUser=GetSessionUser(Token);
            

            CheckSessionStatus(UserSessionStatus);

            CheckPrivilege(PrivilegeID);
        }

        private ViewSessionUserModel GetSessionUser(string token)
        {
            ViewSessionUserModel sessionModel;

            using (Controller.Session controllerSession = new Controller.Session())
            {
                sessionModel = controllerSession.GetSessionUser(Token);
                
            }


            return sessionModel;
        }

        protected virtual void Write(string s)
        {
            this.Response.Write(s);
        }

        protected void WriteSuccess<T>(T t)
        {
            ResponseModel<T> response = new ResponseModel<T>(t,true,RESPONSE_CODE.SUCCESS);

            WriteJson(response);
        }

        protected void WriteEnd()
        {
            Response.End();
        }

        protected void WriteUnfulfil(string unfulfilledMessage)
        {
            ResponseModel<string> response = new ResponseModel<string>(unfulfilledMessage, true, RESPONSE_CODE.UNFULFIL);

            WriteJson(response);
        }

        protected void WriteFail(int errorCode,string errorMessage)
        {
            Hashtable ht = new Hashtable();

            ht.Add("ErrorCode", errorCode);
            ht.Add("ErrorMessage", errorMessage);

            ResponseModel<Hashtable> response = new ResponseModel<Hashtable>(ht, false,RESPONSE_CODE.FAIL);

            WriteJson(response);
        }

        protected void WriteFail(String LangPath)
        {
            int errorCode = Lang.getCode(LangPath);
            string errorMessage = Lang.get(LangPath);
            WriteFail(errorCode, errorMessage);
        }

        protected void WriteException(Exception ex)
        {
            ResponseModel<Exception> response = new ResponseModel<Exception>(ex, false,RESPONSE_CODE.EXCEPTION);

            WriteJson(response);
        }

        protected void WriteJson(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            System.Diagnostics.Debug.WriteLine("JSON Response: " + json);
            this.Response.Write(json);
        }

        protected uint GetUserPrivilege(string Token)
        {
            uint aid;
            uint pid;

            using (Controller.Session controllerSession = new Controller.Session())
            {
                aid = controllerSession.GetAccountId(Token);
            }

            using (Controller.Account controllerAccount = new Controller.Account())
            {
                pid = controllerAccount.GetAccountPrivilege(aid);
            }

            return pid;
        }

        protected void CheckPrivilege(uint PrivilegeID)
        {
            uint[] PagePrivilege;
            try
            {
                using (Controller.Privilege controllerPrivilege = new Controller.Privilege())
                {
                    PagePrivilege = controllerPrivilege.GetPagePrivilegeList(PageFile);
                }

                if(Array.IndexOf(PagePrivilege,PrivilegeID)<0)
                {
                    WriteFail(tool.Lang.get("/language/privilege/deny"));
                    WriteEnd();
                }
            }
            catch (ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.DisplayMessage);
                WriteEnd();
            }catch(Exception ex)
            {
                WriteException(ex);
                WriteEnd();
            }

        }

        protected void CheckSessionStatus(Controller.Session.SESSION_STATUS SessionStatus)
        {
            switch (SessionStatus)
            {
                case Controller.Session.SESSION_STATUS.AVAILABLE:
                    using (Controller.Session controllerSession = new Controller.Session())
                    {
                        controllerSession.Keepalive(Token);
                    }
                    break;
                case Controller.Session.SESSION_STATUS.NOTEXIST:
                case Controller.Session.SESSION_STATUS.EXPIRED:
                case Controller.Session.SESSION_STATUS.UNAVAILABLE:
                case Controller.Session.SESSION_STATUS.UNKNOW:
                    WriteFail((int)SessionStatus, tool.Lang.get("/language/session/session_notavailable"));
                    WriteEnd();
                    break;
            }
        }

        protected uint? getParameterUint(string key)
        {
            string value = this.getParameter(key);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return uint.Parse(value);
            }
            else
            {
                return null;
            }
            
        }

        protected string getParameter(string key)
        {
            if (this.Request[key] != null)
            {
                return this.Request[key].Trim();
            }
                
            if(this.Request.QueryString[key]!=null)
            {
                return this.Request.QueryString[key].Trim();
            }

            if(this.Request.Cookies[key]!=null)
            {
                return this.Request.Cookies[key].Value.Trim();
            }

            return null;
        }

        protected bool IsSetParameter(string key)
        {
            if(getParameter(key)!=null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        protected T getModel<T>()
        {
            return getModel<T>("model");
        }

        protected T getModel<T>(string key)
        {
            string inputStream = getParameter(key);
            string jsonString = HttpContext.Current.Server.UrlDecode(inputStream);
            System.Diagnostics.Debug.WriteLine("Input stream: " + jsonString);
            if(!String.IsNullOrWhiteSpace(jsonString))
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }else
            {
                return default(T);
            }
            
        }

        protected string getInputStream()
        {
            System.IO.StreamReader inputStreamReader = new System.IO.StreamReader(Request.InputStream, PageEncoding);
            return inputStreamReader.ReadToEnd();
        }

        private string getCharset()
        {
            if(PageEncoding == System.Text.Encoding.UTF8)
            {
                return "utf-8";
            }else
            {
                return "utf-8";
            }
        }

    }
}