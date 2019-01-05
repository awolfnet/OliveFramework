using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public abstract class BaseController:IDisposable
    {
        protected Database db;

        public BaseController()
        {
            string dbType= tool.WebConfig.get("db_type");
            string dbServer = tool.WebConfig.get("db_server");
            string dbUid = tool.WebConfig.get("db_uid");
            string dbPwd = tool.WebConfig.get("db_pwd");
            string dbName = tool.WebConfig.get("db_database");
            uint dbTimeout = uint.Parse( tool.WebConfig.get("db_timeout"));

            OpenDatabase(dbType);

            db.Connect(dbServer, dbUid, dbPwd, dbName, dbTimeout);
            
        }

        ~BaseController()
        {
            db.Dispose();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void OpenDatabase(string dbType)
        {
            if (dbType.ToLower().Equals("mysql"))
            {
                db = new MysqlHelper();
            }
            else
            {
                //TODO: 没有配置数据库时异常
                throw new NotImplementedException();
            }
        }

    }
}