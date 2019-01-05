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
    /// GetCargoList 的摘要说明
    /// </summary>
    public class GetCargoList : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                string cargoPlate = getParameter("cargo_plate");
                string cargoDriver = getParameter("cargo_driver");
                string cargoLicence = getParameter("cargo_licence");
                string cargoContact = getParameter("cargo_contact");
                string cargoDescription = getParameter("cargo_description");
                string marketName = getParameter("market_name");

                CargoListModel cargoList = null;

                using (Controller.Cargo controllerCargo = new Controller.Cargo())
                {
                    cargoList = controllerCargo.GetCargoList(cargoPlate, cargoDriver, cargoLicence, cargoContact, cargoDescription, marketName);
                }

                DatagridModel<ViewCargoListModel> data = new DatagridModel<ViewCargoListModel>();
                data.total = (uint)cargoList.Count;
                data.rows = cargoList;

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