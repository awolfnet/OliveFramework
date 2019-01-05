using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// CreateAccount 的摘要说明
    /// </summary>
    public class CreateAccount : BasePage
    {
        protected override void OnRequest()
        {

            AccountRegisterInfo accountInfo = getModel<AccountRegisterInfo>();

            uint aid = 0;
            SessionModel session = null;

            try
            {
                if(accountInfo==null)
                {
                    throw new ExceptionMessage("/language/register/account_info_not_complete");
                }

                if (string.IsNullOrWhiteSpace(accountInfo.account_name))
                {
                    throw new ExceptionMessage("/language/register/need_account_name");
                }

                if (string.IsNullOrWhiteSpace(accountInfo.account_password))
                {
                    throw new ExceptionMessage("/language/register/need_password");
                }

                if(string.IsNullOrWhiteSpace(accountInfo.account_nickname))
                {
                    throw new ExceptionMessage("/language/register/need_nickname");
                }

                AccountModel account = new AccountModel();
                account.Name = accountInfo.account_name;
                account.Password = accountInfo.account_password;
                account.Mail = accountInfo.account_mail;

                aid = new Controller.Account().AddAccount(account);

                if(!string.IsNullOrWhiteSpace(accountInfo.account_nickname))
                {
                    ProfileModel profile = new ProfileModel();
                    profile.profileid = null;
                    profile.Nickname = accountInfo.account_nickname;
                    profile.Avatar = null;
                    uint profileid = new Controller.Account().AddProfile(profile);
                    AccountModel accountUpdate = new AccountModel();
                    accountUpdate.aid = aid;
                    accountUpdate.profileid = profileid;
                    new Controller.Account().UpdateAccountInfo(accountUpdate);
                }

                session = new Controller.Session().CreateSession(accountInfo.account_name, aid);

                LoginResponseModel userLogin = new LoginResponseModel();
                userLogin.AccountID = aid;
                userLogin.Username = accountInfo.account_name;
                userLogin.Token = session.Token;
                userLogin.Expire = session.ValidDay.ToString();

                userLogin.Redirect = "Main.html";

                WriteSuccess<LoginResponseModel>(userLogin);
            }
            catch (UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }
            catch (ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.DisplayMessage);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
            finally
            {

            }
        }


    }
}