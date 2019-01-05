using OliveFramework.Model.Datatable;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.Controller
{
    public class Privilege: BaseController
    {
        public uint[] GetPagePrivilegeList(string Page)
        {
            string where = string.Format("method.File='{0}'", Page);
            List<MethodModel> list = db.SelectData<MethodModel>("method", where);

            if (list == null)
            {
                throw new ExceptionMessage("/language/privilege/not_define");
            }

            string[] p = list[0].Privilege.Split(',');

            uint[] privilegeList = new uint[p.Length];
            int i = 0;
            for(i=0;i<p.Length;i++)
            {
                privilegeList[i] = uint.Parse(p[i]);
            }

            return privilegeList;
        }
    }
}