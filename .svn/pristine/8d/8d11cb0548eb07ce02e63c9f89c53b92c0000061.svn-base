using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// GetMarketList 的摘要说明
    /// </summary>
    public class GetMarketList : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            string marketLocation = getParameter("location");

            try
            {
                MarketListModel marketList = null;
                using (Controller.Market controllerMarket = new Controller.Market())
                {
                    marketList = controllerMarket.GetMarketList(marketLocation, null);
                }

                WriteJson(marketList);
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