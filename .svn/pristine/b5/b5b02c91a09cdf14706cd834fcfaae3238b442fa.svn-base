using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetProductList 的摘要说明
    /// </summary>
    public class GetProductList : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            string marketType = getParameter("type");

            ProductListModel marketList = null;
            using (Controller.Market controllerMarket = new Controller.Market())
            {
                marketList = controllerMarket.GetProductList(marketType);
            }
                
            WriteSuccess<ProductListModel>(marketList);
        }

    }
}