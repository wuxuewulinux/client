using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

public class nameconfig
{
	public short grade;                  //称号等级

	public int shengchang;             //胜场数

	public string name;                 //获得的游戏称号
  
}

public class MingChengConfig
{
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

        //读取胜场获得的属性XML配置
        {
            XmlNode server_element = root.SelectSingleNode("name");        //拿到server的配置
            if (server_element == null)
            {
                Debug.Log("MingChengConfig server_element node error !.");
                return false;
            }
            //进入配置函数进行读取
            iRet = InitMingChengConfig(server_element);
            if (iRet < 0)
            {
                Debug.Log("InitMingChengConfig error !: " + iRet);
                return false;
            }
        }

        //如果该配置模块还需要增加某个功能的配置就在这里增加

        return true;
    }



public int InitMingChengConfig(XmlNode RootElement)   //读取配置信息
{
    XmlNodeList RootList = RootElement.ChildNodes;   //拿到该节点的所有子节点，也就是说拿到所有 "data"节点
    if (RootList == null)
    {
        return -1;
    }
    foreach (XmlNode child in RootList)              //开始遍历所有"data"节点
    {
        nameconfig sName = new nameconfig();
        XmlNodeList childList = child.ChildNodes;    //拿到该"data"节点的所有子节点
        short Grade = 0;
        Grade = Convert.ToInt16(childList.Item(0).InnerText);
        sName.grade = Grade;
        sName.shengchang = Convert.ToInt32(childList.Item(1).InnerText);
        sName.name = childList.Item(2).InnerText;
        NameMap[Grade] = sName;                                 //压入一个节点的数据到容器中
    }

    return 0;
 }



public nameconfig GetNameConfig(short uiKey)
{
    if(NameMap.ContainsKey(uiKey))          //检查是否有该键值
    {
        return NameMap[uiKey];
    }
    return null;

}

//构造函数必须实例化对象

 public MingChengConfig(){
   NameMap = new Dictionary<short, nameconfig>();
}

  //配置类的私有数据

  private Dictionary<short, nameconfig> NameMap;

}

