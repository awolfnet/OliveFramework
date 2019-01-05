using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Account:BaseController
    {
        public uint AddAccount(AccountModel account)
        {
            AccountModel newAccount = new AccountModel();
            newAccount.Name = account.Name;
            newAccount.Password = Hash.MD5(account.Password).ToString().ToUpper();
            newAccount.Mail = account.Mail;
            newAccount.pid = 2;

            uint aid = 0;

            try
            {
                db.InsertSingleLine<AccountModel>("Account", newAccount);
                aid = CheckAccountPassword(account.Name, account.Password);
            }
            catch (Database.Exception ex)
            {
                switch (ex.Code)
                {
                    case 1062:
                        throw new UnfulfilException("/language/database/duplicate_entry");
                    default: throw (ex);
                }
            }

            return aid;
        }

        public uint AddProfile(ProfileModel profile)
        {
            uint profileid = 0;

            try
            {
                db.InsertSingleLine<ProfileModel>("profile", profile);
                profileid=db.GetLastInsertID();
            }
            catch (Database.Exception ex)
            {
                switch (ex.Code)
                {
                    case 1062:
                        throw new UnfulfilException("/language/database/duplicate_entry");
                    default: throw (ex);
                }
            }

            return profileid;
        }

        public int ChangePassword(string accountName, string accountPassword)
        {
            string where = String.Format("account.Name = '{0}' OR account.Mail = '{0}'", accountName);

            AccountModel account = new AccountModel();
            account.Password = Hash.MD5(accountPassword).ToString().ToUpper();

            return db.UpdateSingleLine<AccountModel>("Account", account, where);
        }

        public int UpdateAccountInfo(AccountModel account)
        {
            string where = String.Format("account.aid={0}", account.aid);
            return db.UpdateSingleLine<AccountModel>("Account", account, where);
        }

        public int UpdateProfile(ProfileModel profile)
        {
            string where = String.Format("profile.profileid={0}", profile.profileid);
            return db.UpdateSingleLine<ProfileModel>("profile", profile, where);
        }

        public uint CheckAccountPassword(string accountName,string accountPassword)
        {
            List<AccountModel> accountList = null;
            string where = String.Format("account.Name = '{0}' OR account.Mail = '{0}'", accountName);

            accountList = db.SelectData<AccountModel>("Account", where);

            if(accountList==null)
            {
                throw new ExceptionMessage("/language/login/user_not_exist");
            }

            uint aid = (uint)accountList[0].aid;
            string aPassword = accountList[0].Password;

            if (aPassword.ToUpper().Equals(Hash.MD5(accountPassword).ToString().ToUpper()))
            {
                return aid;
            }
            else
            {
                throw new ExceptionMessage("/language/login/password_not_match");
            }
        }

        public UserinfoModel GetUserinfo(uint aid)
        {
            UserinfoModel userInfo = null;
            string where = String.Format("view_userinfo.aid={0}", aid);
            List<ViewUserinfoModel> list = db.SelectData<ViewUserinfoModel>("view_userinfo", where);
            if(list!=null)
            {
                userInfo = new UserinfoModel();
                ViewUserinfoModel userinfoModel = list[0];

                if (DateTime.Compare((DateTime)userinfoModel.MemberValid, DateTime.Now) > 0)
                {
                    userInfo.memberValid = true;
                }
                else
                {
                    userInfo.memberValid = false;
                }

                userInfo.userAvatar = userinfoModel.Avatar;
                userInfo.userID = (uint)userinfoModel.aid;
                userInfo.userMail = userinfoModel.Mail;
                userInfo.userNickname = userinfoModel.Nickname;
                userInfo.userPhone = userinfoModel.Name;
            }

            return userInfo;
            
        }

        public uint GetAccountPrivilege(uint aid)
        {
            string where = string.Format("Account.aid={0}", aid);
            List<AccountModel> list = db.SelectData<AccountModel>("Account", where);

            return (uint)list[0].pid;
        }

        public uint GetAccountProfileID(uint aid)
        {
            string where = string.Format("Account.aid={0}", aid);
            List<AccountModel> list = db.SelectData<AccountModel>("Account", where);

            return (uint)list[0].profileid;
        }

        public uint GetAccountProfileID(string userName)
        {
            string where = string.Format("Account.Name='{0}'", userName);
            List<AccountModel> list = db.SelectData<AccountModel>("Account", where);

            return (uint)list[0].profileid;
        }

        public List<AccountModel> GetAccountList()
        {
            List<AccountModel> accountList = new List<AccountModel>();

            accountList=db.SelectData<AccountModel>("account");

            return accountList;

        }

        public List<ViewUserinfoModel> GetUserinfoList(string accountName, string accountMail, string accountNickname)
        {
            string where = String.Empty;
            if(!String.IsNullOrWhiteSpace(accountName))
            {
                where = string.Format("view_userinfo.Name like '%{0}%'", accountName);

            }

            if (!String.IsNullOrWhiteSpace(accountMail))
            {
                if (!String.IsNullOrWhiteSpace(where))
                {
                    where += " AND ";
                }
                where+= string.Format("view_userinfo.Mail like '%{0}%'", accountMail);
            }

            if (!String.IsNullOrWhiteSpace(accountNickname))
            {
                if (!String.IsNullOrWhiteSpace(where))
                {
                    where += " AND ";
                }
                where += string.Format("view_userinfo.Nickname like '%{0}%'", accountNickname);
            }

            List<ViewUserinfoModel> userinfoList;
            userinfoList = db.SelectData<ViewUserinfoModel>("view_userinfo",where);
            return userinfoList;
        }

        public List<ViewUserinfoModel> GetUserinfoList()
        {
            return GetUserinfoList(null, null, null);
        }

        public void DeleteAccount(uint accountID)
        {
            string where = string.Format("account.aid={0}", accountID);
            db.DeleteRecord("account", where);
        }
    }

}