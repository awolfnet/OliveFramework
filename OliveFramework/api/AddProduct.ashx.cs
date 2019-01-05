using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// AddProduct 的摘要说明
    /// </summary>
    public class AddProduct : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            string productName = getParameter("product");
            uint marketType = (uint)getParameterUint("market_type");

            try
            {
                new Controller.Market().AddProduct(productName, marketType);
                WriteSuccess<String>("success");
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