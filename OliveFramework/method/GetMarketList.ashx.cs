﻿using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
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
            string marketType = getParameter("type");

            try
            {
                MarketListModel marketList = null;
                using (Controller.Market controllerMarket = new Controller.Market())
                {
                    marketList = controllerMarket.GetMarketList(marketLocation, marketType);
                }
                WriteSuccess<MarketListModel>(marketList);
            } catch (UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }catch(Database.Exception ex)
            {
                WriteException(ex);
            }catch(Exception ex)
            {
                WriteException(ex);
            }
        }

    }
}