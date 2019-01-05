using OliveFramework.Model.Datatable;
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
    /// GetAdList 的摘要说明
    /// </summary>
    public class GetAdList : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                uint? pageLevel = getParameterUint("pagelevel");

                AdListModel adList = null;
                using (Controller.Ad controllerAd = new Controller.Ad())
                {
                    adList = controllerAd.GetAdList(pageLevel);
                }

                DatagridModel<AdModel> data = new DatagridModel<AdModel>();
                data.total = (uint)adList.Count;
                data.rows = adList;

                WriteJson(data);
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