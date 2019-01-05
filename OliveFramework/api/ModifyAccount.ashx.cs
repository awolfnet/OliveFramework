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
    /// ModifyAccount 的摘要说明
    /// </summary>
    public class ModifyAccount : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                AccountModifyInfo accountModify = getModel<AccountModifyInfo>();

                using (Controller.Account accountController = new Controller.Account())
                {
                    if (!string.IsNullOrWhiteSpace(accountModify.account_password))
                    { 
                        accountController.ChangePassword(accountModify.account_name, accountModify.account_password);
                    }
                    ProfileModel profile = new ProfileModel();
                    profile.Nickname = accountModify.account_nickname;
                    profile.MemberValid = DateTime.Parse(accountModify.account_membervalid);

                    profile.profileid = accountController.GetAccountProfileID(accountModify.account_name);
               
                    accountController.UpdateProfile(profile);
                }

                WriteSuccess<string>("success");

            }catch(FormatException ex)
            {
                WriteFail("/language/parameter/format_exception");
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
    }

}