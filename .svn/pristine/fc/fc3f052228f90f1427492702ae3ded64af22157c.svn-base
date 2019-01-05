using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace OliveFramework.tool
{
    public class XMLHelper
    {
        XmlDocument xmlDoc;

        public XMLHelper(string XMLFile)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLFile);


        }

        public string get(string node)
        {
            XmlNode xmlNode;
            string nodeText=null;
            try
            {
                xmlNode = xmlDoc.SelectSingleNode(node);
                nodeText = xmlNode.InnerText;
                System.Diagnostics.Debug.WriteLine(nodeText);
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return nodeText;

        }

        public static string get(string XMLFile,string node)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(SystemConfig.AppDomainAppPath + XMLFile);

            XmlNode xmlNode;
            string nodeText = null;
            try
            {
                xmlNode = xmlDoc.SelectSingleNode(node);
                nodeText = xmlNode.InnerText;
                System.Diagnostics.Debug.WriteLine(nodeText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            return nodeText;

        }

        public static int getCode(string XMLFile, string node)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(SystemConfig.AppDomainAppPath + XMLFile);

            XmlNode xmlNode;
            int nodeCode = 0;
            try
            {
                xmlNode = xmlDoc.SelectSingleNode(node);
                nodeCode = Int32.Parse(xmlNode.Attributes["code"].Value);
                System.Diagnostics.Debug.WriteLine(nodeCode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            return nodeCode;
        }

    }


}