﻿using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetBuyHistory 的摘要说明
    /// </summary>
    public class GetBuyHistory : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                OrderHistoryModel orderHistoryList = new Controller.Order().GetBuyHistory(this.aid);
                WriteSuccess<OrderHistoryModel>(orderHistoryList);
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