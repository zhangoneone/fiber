using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FiberopticServer
{
    public class Xmlopera
    {
        public Xmlopera()
        { }
        public void Delete_Xml()
        {
            if (System.IO.File.Exists(@".\ZoomInfo.xml")) //判断xml文件存在 
            {
                System.IO.File.Delete(@".\ZoomInfo.xml");
            }
        }
        public void Create_Zoom_Xml()
        {
            if (System.IO.File.Exists(@".\ZoomInfo.xml")) //判断xml文件存在 
            {
                return;
            }
            XmlDocument xml = new XmlDocument();
            //3、创建一行声明信息，并添加到 xml 文档顶部
            XmlDeclaration decl = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(decl);

            //4、创建根节点
            XmlElement rootEle = xml.CreateElement("区间配置");
            xml.AppendChild(rootEle);
            xml.Save(@".\ZoomInfo.xml");
        }
        public void Append_Zoom_Xml(importdll.ZoomInfo zoominfo)
        {
            XmlDocument xml = new XmlDocument();
            //加载xml文件
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; //忽略文档里面的注释
            XmlReader reader = XmlReader.Create(@".\ZoomInfo.xml", settings);
            xml.Load(reader);
            reader.Close();
            //加载根节点
            XmlNode rootEle = xml.SelectSingleNode("区间配置");
            //创建子结点 区间号
            XmlElement childEle = xml.CreateElement("区间");
            rootEle.AppendChild(childEle);

            XmlElement c2Ele = xml.CreateElement("区间编号");
            c2Ele.InnerText = zoominfo.ZoomNum.ToString();
            childEle.AppendChild(c2Ele);
            c2Ele = xml.CreateElement("区间名称");
            c2Ele.InnerText = zoominfo.ZoomName;
            childEle.AppendChild(c2Ele);
            c2Ele = xml.CreateElement("区间起点");
            c2Ele.InnerText = zoominfo.ZoomStartLoc.ToString();
            childEle.AppendChild(c2Ele);
            c2Ele = xml.CreateElement("区间终点");
            c2Ele.InnerText = zoominfo.ZoomEndLoc.ToString();
            childEle.AppendChild(c2Ele);
            c2Ele = xml.CreateElement("阈值设置");
            c2Ele.InnerText = zoominfo.Threshold.ToString();
            childEle.AppendChild(c2Ele);
            c2Ele = xml.CreateElement("异常比设置");
            c2Ele.InnerText = zoominfo.ExpRate.ToString();
            childEle.AppendChild(c2Ele);
            c2Ele = xml.CreateElement("确认比设置");
            c2Ele.InnerText = zoominfo.AckRate.ToString();
            childEle.AppendChild(c2Ele);
            xml.Save(@".\ZoomInfo.xml");
        }
        //读xml到zoominfo中
        public int ReadXml(List<importdll.ZoomInfo> zoominfo)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; //忽略文档里面的注释
            try
            {
                XmlReader reader = XmlReader.Create(@".\ZoomInfo.xml", settings);
                xmlDoc.Load(reader);
                reader.Close();
            }
            catch { return -1; }

            importdll.ZoomInfo tmp_zoominfo=new importdll.ZoomInfo();
            //然后可以通过调用SelectSingleNode得到指定的结点,通过GetAttribute得到具体的属性值.参看下面的代码
            // 得到根节点bookstore
            XmlNode mainNote = xmlDoc.SelectSingleNode("区间配置"); //通过根节点来读取XML文件

            if (mainNote != null) //如果根节点存在
            {
                XmlNodeList childNote = mainNote.ChildNodes; //声明一个获取所有子节点的集合
                foreach (XmlNode outchildNote in childNote) //在这个集合中遍历所有子节点
                {
                    XmlElement bookNote = (XmlElement) outchildNote; // 将节点转换为元素，便于得到节点的属性值
                    XmlNodeList singleNote = bookNote.ChildNodes; // 将所有节点属性(也就是最终的值)放进节点集合

                    tmp_zoominfo.ZoomNum = Convert.ToInt32(singleNote[0].InnerText);
                    tmp_zoominfo.ZoomName = singleNote[1].InnerText;
                    tmp_zoominfo.ZoomStartLoc = Convert.ToDouble(singleNote[2].InnerText);
                    tmp_zoominfo.ZoomEndLoc = Convert.ToDouble(singleNote[3].InnerText);
                    tmp_zoominfo.Threshold = Convert.ToDouble(singleNote[4].InnerText);
                    tmp_zoominfo.ExpRate = Convert.ToDouble(singleNote[5].InnerText);
                    tmp_zoominfo.AckRate = Convert.ToDouble(singleNote[6].InnerText);
                    zoominfo.Add(tmp_zoominfo);
                }
            }
            return 0;
        }
    }
}
