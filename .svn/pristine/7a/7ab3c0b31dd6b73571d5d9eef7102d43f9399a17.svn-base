using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// SearchMerchant 的摘要说明
    /// </summary>
    public class SearchMerchant : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();
            string merchantName = getParameter("merchant_name");
            string merchantContact = getParameter("merchant_contact");
            string merchantDescription = getParameter("merchant_description");
            string productName = getParameter("product_name");
            string marketName = getParameter("market_name");

            try
            {
                MerchantListModel merchantList = null;
                using (Controller.Merchant controllerMerchant = new Controller.Merchant())
                {
                    merchantList = controllerMerchant.GetMerchantList(merchantName, merchantContact, merchantDescription, marketName, productName,null);
                }

                DatagridModel<ViewMerchantListModel> data = new DatagridModel<ViewMerchantListModel>();
                data.total = (uint)merchantList.Count();
                data.rows = merchantList;

                WriteJson(data);
            }
            catch (UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }

            WriteEnd();
        }
    }
}