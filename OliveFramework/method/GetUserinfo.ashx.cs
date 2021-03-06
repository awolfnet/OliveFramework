﻿using OliveFramework.Model.Response;
using OliveFramework.Page;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OliveFramework.method
{
    public class GetUserinfo : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();
            UserinfoModel userInfo = null;

            uint? userID = getParameterUint("user_id");

            try
            {
                if (userID == null)
                {
                    throw new UnfulfilException(0, "未指定用户");
                }

                using (Controller.Account controllerAccount = new Controller.Account())
                {
                    userInfo = controllerAccount.GetUserinfo((uint)userID);
                    if (userInfo == null)
                    {
                        throw new UnfulfilException(0, "未找到指定用户");
                    }
                }

                WriteSuccess<UserinfoModel>(userInfo);
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