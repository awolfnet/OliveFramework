﻿using OliveFramework.Controller;
using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Ad: BaseController
    {

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="PageLevel"></param>
        /// <returns></returns>
        public AdListModel GetAdList(uint? PageLevel)
        {
            //List<Dictionary<string, string>> list=new List<Dictionary<string, string>>();

            List<AdModel> list = null;

            if (PageLevel!=null)
            {
                string where = String.Format(" ad.PageLevel={0}", PageLevel);
                list = db.SelectData<AdModel>("ad", where);
            }else
            {
                list = db.SelectData<AdModel>("ad");
            }

            
            AdListModel adList = new AdListModel();

            
            foreach (AdModel ad in list)
            {
                ad.imgsrc= SystemConfig.WebappURL + SystemConfig.AdUploadPath + ad.imgsrc;
                adList.Add(ad);

                //dictionary<string, string> map = new dictionary<string, string>();
                //map.add("link", ad.imglink);

                //string imgsrc =systemconfig.webappurl + systemconfig.aduploadpath+ ad.imgsrc;
                //map.add("source", imgsrc);

                //list.add(map);
            }
            
            
            return adList;
        }

        public void AddAd(string imgsrc,string imglink,string imgalt,uint pagelevel)
        {
            AdModel ad = new AdModel();
            ad.imgsrc = imgsrc;
            ad.imglink = imglink;
            ad.pagelevel = (int)pagelevel;

            db.InsertSingleLine<AdModel>("ad", ad);
        }
 
        public void DeleteAd(uint adid)
        {
            string where = string.Format("ad.adid={0}", adid);
            db.DeleteRecord("ad", where);
        }


    }
}