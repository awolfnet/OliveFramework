using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetCargoList 的摘要说明
    /// </summary>
    public class GetCargoList : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            string marketName = getParameter("market_name");
            string cargoDescription = getParameter("cargo_description");

            CargoListModel cargoList = null;

            try
            {
                cargoList = new Controller.Cargo().GetCargoList(marketName, cargoDescription);
                WriteSuccess<CargoListModel>(cargoList);
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