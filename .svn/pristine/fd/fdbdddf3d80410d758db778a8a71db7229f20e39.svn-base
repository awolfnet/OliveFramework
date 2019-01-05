using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// AddAccount 的摘要说明
    /// </summary>
    public class AddAccount : BasePage
    {

        protected override void OnRequest()
        {

            AccountRegisterInfo accountInfo = getModel<AccountRegisterInfo>();

            uint aid = 0;

            try
            {
                if (accountInfo == null)
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

                if (string.IsNullOrWhiteSpace(accountInfo.account_nickname))
                {
                    throw new ExceptionMessage("/language/register/need_nickname");
                }

                AccountModel account = new AccountModel();
                account.Name = accountInfo.account_name;
                account.Password = accountInfo.account_password;
                account.Mail = accountInfo.account_mail;

                aid = new Controller.Account().AddAccount(account);

                if (!string.IsNullOrWhiteSpace(accountInfo.account_nickname))
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

                WriteSuccess<string>("success");
            }
            catch (UnfulfilException ex)
            {
                WriteFail(ex.Code,ex.DisplayMessage);
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