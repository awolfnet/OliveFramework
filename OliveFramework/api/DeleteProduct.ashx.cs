using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// DeleteProduct 的摘要说明
    /// </summary>
    public class DeleteProduct :  BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            uint productID = (uint)getParameterUint("product_id");

            try
            {
                new Controller.Market().DeleteProduct(productID,null,0);
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

        }
    }
}