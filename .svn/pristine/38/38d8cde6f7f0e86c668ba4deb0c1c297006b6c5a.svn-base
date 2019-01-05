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
    /// GetMerchantType 的摘要说明
    /// </summary>
    public class GetMerchantType : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                List<MerchantTypeModel> merchantTypeList = null;
                using (Controller.Merchant controllerMerchant = new Controller.Merchant())
                {
                    merchantTypeList = controllerMerchant.GetMerchantTypeList();
                }
                WriteJson(merchantTypeList);
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