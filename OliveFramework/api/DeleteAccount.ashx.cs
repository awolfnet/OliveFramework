using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// DeleteAccount 的摘要说明
    /// </summary>
    public class DeleteAccount : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                List<uint> accountList = getModel<List<uint>>("account_list");

                using (Controller.Account controllerAccount = new Controller.Account())
                {
                    foreach (uint accountID in accountList)
                    {
                        if(accountID==1)
                        {
                            throw new Exception("禁止删除管理员账号");
                        }
                        controllerAccount.DeleteAccount(accountID);
                    }
                }

                WriteSuccess<string>("success");

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