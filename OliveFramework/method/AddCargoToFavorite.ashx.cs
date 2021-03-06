﻿using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// AddCargoToFavorite 的摘要说明
    /// </summary>
    public class AddCargoToFavorite : BasePage
    {

        protected override void OnRequest()
        {
            base.OnRequest();

            uint cargoID = 0;

            if (IsSetParameter("cargo_id"))
            {
                cargoID = uint.Parse(getParameter("cargo_id"));
            }
            else
            {
                WriteFail("/language/favorite/need_cargo_id");
                WriteEnd();
            }

            try
            {
                new Controller.Favorite().AddCargo(aid, cargoID);
                WriteSuccess<String>("添加成功");
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