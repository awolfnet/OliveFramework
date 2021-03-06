﻿using Newtonsoft.Json;
using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// AddMerchant 的摘要说明
    /// </summary>
    public class AddMerchant : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();
            
            try
            {
                string merchantName = getParameter("name");
                int? merchantType=int.Parse(getParameter("type"));
                string merchantContact = getParameter("contact");
                string merchantWebsite = getParameter("website");
                uint marketID = (uint)getParameterUint("market");
                string merchantDescription = getParameter("description");
                List<IDText> merchantSellProductList = getModel<List<IDText>>("sell");
                uint? merchantOwner = getParameterUint("owner");

                using (Controller.Merchant controllerMerchant= new Controller.Merchant())
                {
                    controllerMerchant.AddMerchant(merchantName, merchantContact, merchantWebsite, merchantDescription, merchantSellProductList, marketID, merchantOwner, merchantType);
                }

                WriteSuccess<String>("success");
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