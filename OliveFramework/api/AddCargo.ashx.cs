﻿using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// AddCargo 的摘要说明
    /// </summary>
    public class AddCargo : BasePage
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
                uint? cargoOwnerID = getParameterUint("cargo_owner");
                uint marketID = (uint)getParameterUint("market");


                using (Controller.Cargo controllerCargo = new Controller.Cargo())
                {
                    controllerCargo.AddCargo(cargoPlate, cargoDriver, cargoLicence, cargoContact, cargoDescription, marketID, cargoOwnerID);
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