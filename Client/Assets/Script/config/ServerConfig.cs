using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

public class my_server
{
	public string ip;             //获取服务器IP地址

	public int port;                  //获取服务器端口号

}

public class ServerConfig
{
//构造函数必须实例化对象

 public ServerConfig()
{
    sServer = new my_server();
}

public bool Init(string configname)
       {
           int iRet;
           //加载XML文件
           XmlDocument doc = new XmlDocument();
           doc.Load(configname);

           XmlNode root = doc.SelectSingleNode("root");     //想切换到根节点
           if (root == null)
           {
               Debug.Log("ServerConfig root node error !.");
               return false;
           }

           //读取服务器的IP地址和端口号的XML配置
           {
               XmlNode server_element = root.SelectSingleNode("server");        //拿到server的配置
               if (server_element == null)
               {
                   Debug.Log("ServerConfig server_element node error !.");
                   return false;
               }
               //进入配置函数进行读取
               iRet = InitServerConfigg(server_element);
               if (iRet < 0)
               {
                   Debug.Log("InitServerConfigg error !: " + iRet);
                   return false;
               }

           }

           //如果这张XML文件还有别的模块配置就在这里增加


           return true;
       }

public int InitServerConfigg(XmlNode RootElement)
   {
           XmlNodeList RootList = RootElement.ChildNodes;   //拿到该节点的所有子节点，也就是说拿到所有 "data"节点
           if (RootList == null)
           {
               return -1;
            }
           foreach (XmlNode child in RootList)              //开始遍历所有"data"节点
           {
               XmlNodeList childList = child.ChildNodes;    //拿到该"data"节点的所有子节点
               //开始读取真实的数据
               if (childList.Item(0).InnerText == null)     //对节点数据进行异常判断
                   return -2;
               sServer.ip = childList.Item(0).InnerText;    //获取第一个值 
               if (childList.Item(1).InnerText == null)     //对节点数据进行异常判断
                   return -3;
               sServer.port = Convert.ToInt32(childList.Item(1).InnerText); //获取第2个值，默认是string类型，所有必须转换成int类型
           }

           return 0;
   }

 //获取Server配置数据结构体
 public my_server GetServerConfig() { return sServer;}


//类数据都在一下填写

private my_server sServer;           //Server配置数据保存在该结构体中


   }

