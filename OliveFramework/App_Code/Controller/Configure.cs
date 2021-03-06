﻿using OliveFramework.Model.Datatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Configure:BaseController
    {
        private List<ConfigureModel> _getConf(string KEY)
        {
            string where = string.Format("Configure.KEY='{0}'", KEY);

            List<ConfigureModel> list=db.SelectData<ConfigureModel>("Configure", where);

            if(list==null)
            {
                throw new ExceptionMessage("/language/config/configkey_not_found");
            }

            return list;
            
        }

        public bool isSet(String KEY)
        {
            bool b = false;
            try
            {
                List<ConfigureModel> list = _getConf(KEY);
                string value = list[0].VALUE;
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ExceptionMessage("/language/config/config_empty");
                }
                b = true;
            }catch(ExceptionMessage ex)
            {
                b = false;
            }

            return b;
        }

        public int? getInt(string KEY)
        {
            List<ConfigureModel> list = _getConf(KEY);
            string value = list[0].VALUE;
            if (String.IsNullOrWhiteSpace(value))
            {
                return null;
            }else
            {
                return int.Parse(value);
            }
        }

        public uint? getUint(String KEY)
        {
            List<ConfigureModel> list = _getConf(KEY);
            string value = list[0].VALUE;
            if (String.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else
            {
                return uint.Parse(value);
            }
        }

        public string getString(string KEY)
        {
            List<ConfigureModel> list = _getConf(KEY);
            string value = list[0].VALUE;
            if (String.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else
            {
                return value;
            }
        }
            
    }
}