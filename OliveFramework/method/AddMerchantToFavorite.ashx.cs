﻿using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// AddMerchantToFavorite 的摘要说明
    /// </summary>
    public class AddMerchantToFavorite : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            uint merchantID = 0;

            if (IsSetParameter("merchant_id"))
            {
                merchantID = uint.Parse(getParameter("merchant_id"));
            }else
            {
                WriteFail("/language/favorite/need_merchant_id");
                WriteEnd();
            }

            try
            {
                new Controller.Favorite().AddMerchant(aid, merchantID);
                WriteSuccess<String>("添加成功");
            }catch(UnfulfilException ex)
            {
                WriteUnfulfil(ex.DisplayMessage);
            }catch(Database.Exception ex)
            {
                WriteException(ex);
            }
            catch(Exception ex)
            {
                WriteException(ex);
            }

        }


    }
}