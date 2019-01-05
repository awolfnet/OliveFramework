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
    /// GetFavoriteList 的摘要说明
    /// </summary>
    public class GetMerchantFavoriteList : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            MerchantFavoriteListModel favoriteList = null;

            try
            {
                favoriteList = new Controller.Favorite().GetMerchantFavoriteList(aid);
                WriteSuccess<MerchantFavoriteListModel>(favoriteList);
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