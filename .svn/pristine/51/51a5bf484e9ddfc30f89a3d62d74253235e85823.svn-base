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
    /// GetAccountList 的摘要说明
    /// </summary>
    public class GetAccountList : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                List<AccountModel> accountList = null;
                using (Controller.Account controllerAccount= new Controller.Account())
                {
                    accountList=controllerAccount.GetAccountList();
                }
                WriteJson(accountList);
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