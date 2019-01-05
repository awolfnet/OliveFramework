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
    /// GetMerchantList 的摘要说明
    /// </summary>
    public class GetMerchantList : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            string productName = getParameter("product_name");
            string marketName = getParameter("market_name");

            try
            {
                MerchantListModel merchantList = null;
                using (Controller.Merchant controllerMerchant = new Controller.Merchant())
                {
                    if(MemberValid==true)
                    {
                        merchantList = controllerMerchant.GetMerchantList(productName, marketName);
                    }
                    else
                    {
                        merchantList = controllerMerchant.GetMerchantList(productName, marketName,2);
                    }
                    
                }
                WriteSuccess<MerchantListModel>(merchantList);
            }
            catch(UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }catch(Exception ex)
            {
                WriteException(ex);
            }

            WriteEnd();
        }
    }
}