using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// DeleteMerchant 的摘要说明
    /// </summary>
    public class DeleteMerchant : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                List<uint> merchantList = getModel<List<uint>>("merchant_list");

                foreach(uint merchantID in merchantList)
                {
                    new Controller.Merchant().DeleteMerchant(merchantID);
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