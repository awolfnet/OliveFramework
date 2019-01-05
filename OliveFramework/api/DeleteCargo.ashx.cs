using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.api
{
    /// <summary>
    /// DeleteCargo 的摘要说明
    /// </summary>
    public class DeleteCargo : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                List<uint> cargoList = getModel<List<uint>>("cargo_list");

                using (Controller.Cargo controllerCargo=new Controller.Cargo())
                {
                    foreach (uint cargoID in cargoList)
                    {
                        controllerCargo.DeleteCargo(cargoID);
                    }
                }
                
                WriteSuccess<string>("success");

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