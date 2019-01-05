using OliveFramework.Model.Datatable;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// GetUserinfoList 的摘要说明
    /// </summary>
    public class GetUserinfoList : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                string accountName = getParameter("account_name");
                string accountMail = getParameter("account_mail");
                string accountNickname = getParameter("account_nickname");

                List<ViewUserinfoModel> userinfoList = null;
                using (Controller.Account controllerAccount = new Controller.Account())
                {
                    userinfoList = controllerAccount.GetUserinfoList(accountName, accountMail, accountNickname);
                }
                WriteJson(userinfoList);
            }
            catch (UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }
            catch (Database.Exception ex)
            {
                WriteException(ex);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
    }
}