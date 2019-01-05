using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OliveFramework
{
    public partial class AdPage : System.Web.UI.Page
    {
        public string img_src = "./upload/ad/2018_10_22110628771.jpg";
        public string a_href = "http://www.baidu.com";

        protected void Page_Load(object sender, EventArgs e)
        {
            string page = Request.QueryString["page"];
            if( string.IsNullOrWhiteSpace(page))
            {
                Response.End();
            }

            uint pagelevel = 0;
            switch( page)
            {
                case "city":
                    pagelevel = 10;
                    break;
                case "home":
                    pagelevel = 11;
                    break;
                case "mine":
                    pagelevel = 12;
                    break;
            }
            try
            {
                using (Controller.Ad controllerAd = new Controller.Ad())
                {
                    OliveFramework.Model.Response.AdListModel adList = controllerAd.GetAdList(pagelevel);
                    img_src = adList[0].imgsrc;
                    a_href = adList[0].imglink;
                }
            }
            catch(Exception ex)
            {
                Response.End();

            }
           

        }
    }
}