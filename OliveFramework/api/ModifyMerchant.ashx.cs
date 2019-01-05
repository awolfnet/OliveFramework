using Newtonsoft.Json;
using OliveFramework.Model.Datatable;
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
    /// AddMerchant 的摘要说明
    /// </summary>
    public class ModifyMerchant : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();
            
            try
            {
                uint? merchantID= getParameterUint("merchant_id");
                string merchantName = getParameter("name");
                string merchantContact = getParameter("contact");
                string merchantWebsite = getParameter("website");
                uint marketID = (uint)getParameterUint("market");
                string merchantDescription = getParameter("description");
                List<IDText> merchantSellProductList = getModel<List<IDText>>("sell");
                uint? merchantOwner = getParameterUint("owner");

                if(merchantID==null)
                {
                    throw new UnfulfilException(65535, "商户ID为空");

                }

                if(merchantOwner==null)
                {
                    throw new UnfulfilException(65535, "商户所有者为空");
                }

                using (Controller.Merchant controllerMerchant= new Controller.Merchant())
                {
                    controllerMerchant.ModifyMerchant((uint)merchantID, merchantName, merchantContact, merchantWebsite, merchantDescription, merchantSellProductList, marketID, merchantOwner);
                    //controllerMerchant.AddMerchant(merchantName, merchantContact, merchantWebsite, merchantDescription, merchantSellProductList, marketID, merchantOwner);
                }

                WriteSuccess<String>("success");
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