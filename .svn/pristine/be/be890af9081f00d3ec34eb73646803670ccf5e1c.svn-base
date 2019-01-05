using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetSellCargoHistory 的摘要说明
    /// </summary>
    public class GetSellCargoHistory : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                CargoOrderHistoryModel orderHistoryList = new Controller.Order().GetSellCargoHistory(this.aid);
                WriteSuccess<CargoOrderHistoryModel>(orderHistoryList);
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