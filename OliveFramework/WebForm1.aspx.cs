﻿using OliveFramework.Controller;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OliveFramework
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.CheckUserSession();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            object o= new Controller.Configure().getString("weburl");
            System.Diagnostics.Debug.WriteLine(o.ToString());
        }
    }
}