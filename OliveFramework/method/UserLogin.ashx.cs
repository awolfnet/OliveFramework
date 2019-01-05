using Newtonsoft.Json;
using OliveFramework.tool;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Web;
using OliveFramework;
using OliveFramework.Page;
using OliveFramework.Controller;
using OliveFramework.Model.Datatable;

namespace OliveFramework.method
{
    /// <summary>
    /// UserLogin 的摘要说明
    /// </summary>
    public class UserLogin : BasePage
    {
        protected override void OnRequest()
        {
            string accountName = getParameter("account");
            string accountPassword = getParameter("password");
            
            uint aid = 0;
            SessionModel session = null;
            try
            {
                aid = new Controller.Account().CheckAccountPassword(accountName, accountPassword);
                session =new Controller.Session().CreateSession(accountName, aid);

                LoginResponseModel userLogin = new LoginResponseModel() ;
                userLogin.AccountID = aid;
                userLogin.Username = accountName;
                userLogin.Token = session.Token;
                userLogin.Expire = session.ValidDay.ToString();

                userLogin.Redirect = "Main.html";

                WriteSuccess<LoginResponseModel>(userLogin);

            }
            catch (ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.DisplayMessage);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }finally
            {

            }

        }

    }
}