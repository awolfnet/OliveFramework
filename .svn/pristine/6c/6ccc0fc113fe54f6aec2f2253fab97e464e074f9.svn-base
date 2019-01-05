using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// DeleteMarket 的摘要说明
    /// </summary>
    public class DeleteMarket : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            uint marketID = (uint)getParameterUint("market_id");

            try
            {
                new Controller.Market().DeleteMarket(marketID);
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