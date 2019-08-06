using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Aaron.Common
{
    public class XMLHelper
    {
        #region MyRegion
        //xml格式前面没有勾看着不爽...改成config后缀一样能被读取只要格式一样就可以了
        private static string filePath = @"D:\Mirror\Aaron.Common\Aaron.Common\XML和配置文件\config.config";

        /// <summary>
        /// 根据传入路径读取出XML的值  必须有个根节点 如configuration/SQLConnection
        /// </summary>
        /// <param name="xPath">遵循xPath规则可以一路查下去  范例: @"Skill/First/SkillItem"</param>
        /// <returns></returns>
        public static string GetNodeText(string xPath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                return xn.FirstChild.InnerText; //得到该节点的子节点

            }
            catch
            {
                return null;
            }
        }

        public static void UpdateXMLList(List<string> list, string path, string listItem = "")
        {
            //如果为空,就说明我是想直接更新list而已
            if (listItem != "")
            {
                //如果已经存在就不处理,不存在就添加
                if (!list.Contains(listItem))
                {
                    if (list.Count == 5)
                    {
                        list.RemoveAt(0);
                        list.Insert(0, listItem);
                    }
                    else
                    {
                        list.Insert(0, listItem);
                    }
                }
                else
                {
                    //如果存在要把他提到最前,按钮也会生成到第一个  可能会造成有些是 .last的报错,直接改成frist就可以了
                    list.Remove(listItem);
                    list.Insert(0, listItem);
                }
            }
            // List 转成json以后会自带一个, 
            var jsonStr = JsonHelper.SerializeObject(list);
            UpdateNodeInnerText(path, jsonStr);
        }

        /// <summary>
        /// 修改节点的InnerText的值
        /// </summary>
        /// <param name="filePath">XML文件绝对路径</param>
        /// <param name="xPath">范例: @"Skill/First/SkillItem"</param>
        /// <param name="value">节点的值</param>
        /// <returns></returns>
        public static bool UpdateNodeInnerText(string xPath, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                XmlElement xe = (XmlElement)xn;
                xe.InnerText = value;
                doc.Save(filePath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        public static object LoadFromXml(string filePath, Type type)
        {
            object result = null;
            bool flag = File.Exists(filePath);
            if (flag)
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    result = xmlSerializer.Deserialize(streamReader);
                }
            }
            return result;
        }
        public static void SaveToXml(string filePath, object sourceObj, Type type, string xmlRootName)
        {
            bool flag = !string.IsNullOrEmpty(filePath) && sourceObj != null;
            if (flag)
            {
                type = ((type != null) ? type : sourceObj.GetType());
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add("", "");
                    XmlSerializer xmlSerializer = string.IsNullOrEmpty(xmlRootName) ? new XmlSerializer(type) : new XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(streamWriter, sourceObj, xmlSerializerNamespaces);
                }
            }
        }

    }
}