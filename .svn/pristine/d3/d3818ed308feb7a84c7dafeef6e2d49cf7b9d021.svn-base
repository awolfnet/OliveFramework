using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// AddMarket 的摘要说明
    /// </summary>
    public class AddMarket : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            string marketProvince= getParameter("market_province");
            string marketName=getParameter("market_name");
            uint marketType =(uint)getParameterUint("market_type");

            try
            {
                new Controller.Market().AddMarket(marketProvince, marketName, marketType);
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