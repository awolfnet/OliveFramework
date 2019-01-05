using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Model.Datatable
{
    public class SessionModel
    {
        /// <summary>
        /// Session id
        /// </summary>
        public uint? sid { set; get; }
        /// <summary>
        /// Account id
        /// </summary>
        public uint? aid { set; get; }
        /// <summary>
        /// Last active
        /// </summary>
        public DateTime? LastActive { set; get; }
        /// <summary>
        /// Session token
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        /// Session valid day
        /// </summary>
        public DateTime? ValidDay { set; get; }
        /// <summary>
        /// Session create daytime
        /// </summary>
        public DateTime? CreateDay { set; get; }
    }
}