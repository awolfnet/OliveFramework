
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.tool
{
    public class Lang
    {
        /// <summary>
        /// 从语言文件中获得显示内容
        /// </summary>
        /// <param name="path">内容路径："root/path/node"</param>
        /// <returns></returns>
        public static string get(string path)
        {
            string LangFilepath = SystemConfig.LangugeFilePath;
            return OliveFramework.tool.XMLHelper.get(LangFilepath, path);
        }

        public static int getCode(string path)
        {
            string LangFilepath = SystemConfig.LangugeFilePath;
            return OliveFramework.tool.XMLHelper.getCode(LangFilepath, path);
        }
    }
}