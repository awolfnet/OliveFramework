﻿using OliveFramework.Model.Datatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Session:BaseController
    {
        public enum SESSION_STATUS
        {
            UNKNOW = 0,
            AVAILABLE,
            UNAVAILABLE,
            NOTEXIST,
            EXPIRED ,
        }

        public SessionModel CreateSession(string accountName,uint aid)
        {
            SessionModel session = new SessionModel();
            int validday = SystemConfig.TokenValidDay;
            session.ValidDay = DateTime.Now.AddDays(validday);
            session.aid = aid;
            session.Token = MakeToken(accountName, aid.ToString());
            session.LastActive = DateTime.Now;
            session.CreateDay = DateTime.Now;
            db.InsertSingleLine<SessionModel>("Session", session);

            return GetSession(session.Token);
        }

        public SessionModel GetSession(string Token)
        {
            List<SessionModel> list = db.SelectData<SessionModel>("Session", string.Format("Session.Token='{0}'", Token));
            if (list!=null)
            {
                return list[0];
            }else
            {
                return null;
            }
        }

        public ViewSessionUserModel GetSessionUser(string Token)
        {
            List<ViewSessionUserModel> list = db.SelectData<ViewSessionUserModel>("view_sessionuser", string.Format("view_sessionuser.Token='{0}'", Token));
            if (list != null)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public SESSION_STATUS GetSessionStatus(string Token)
        {
            SESSION_STATUS sessionStatus = SESSION_STATUS.UNKNOW;
            SessionModel session=GetSession(Token);
            if(session==null)
            {
                sessionStatus = SESSION_STATUS.NOTEXIST;
                return sessionStatus;
            }

            if(DateTime.Compare((DateTime)session.ValidDay, DateTime.Now)>0)
            {
                sessionStatus = SESSION_STATUS.AVAILABLE;
            }else
            {
                sessionStatus = SESSION_STATUS.EXPIRED;
            }
            
            return sessionStatus;
        }

        public int Keepalive(string Token)
        {
            SessionModel session = new SessionModel();

            session.aid = null;
            session.sid = null;
            session.Token = null;
            session.ValidDay = null;
            session.CreateDay = null;
            session.LastActive = DateTime.Now;

            return db.UpdateSingleLine<SessionModel>("Session", session, string.Format("Session.Token='{0}'", Token));
        }

        public uint GetAccountId(string Token)
        {
            string where = string.Format("Session.Token='{0}'", Token);

            List<SessionModel> list = db.SelectData<SessionModel>("Session", where);
            return (uint)list[0].aid;
        }

        public List<SessionModel> ListAllSession()
        {
            return db.SelectData<SessionModel>("Session");
        }

        private string MakeToken(string accountName, string salt)
        {
            string seed = accountName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + salt;

            return tool.Hash.MD5(seed);
        }


    }
}