using OliveFramework.Model.Request;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// GetMerchantSell 的摘要说明
    /// </summary>
    public class GetMerchantSell : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                uint merchantID = (uint)getParameterUint("merchant_id");
                List<IDText> merchantSellList = new Controller.Merchant().GetMerchantSell(merchantID);
                WriteJson(merchantSellList);
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