using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class ViewSessionUserModel
    {
        /// <summary>
        /// Session token
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        /// Last active
        /// </summary>
        public DateTime? LastActive { set; get; }
        /// <summary>
        /// Session valid day
        /// </summary>
        public DateTime? ValidDay { set; get; }
        /// <summary>
        /// Session create daytime
        /// </summary>
        public DateTime? CreateDay { set; get; }
        /// <summary>
        /// Login name
        /// </summary>
        public String Name { set; get; }
        /// <summary>
        /// Mail
        /// </summary>
        public string Mail { set; get; }
        /// <summary>
        /// Nick name
        /// </summary>
        public string Nickname { set; get; }
        /// <summary>
        /// Member valid date
        /// </summary>
        public DateTime? MemberValid { set; get; }
        /// <summary>
        /// Privilege id
        /// </summary>
        public uint? pid { set; get; }
        /// <summary>
        /// Account id
        /// </summary>
        public uint? aid { set; get; }




    }
}