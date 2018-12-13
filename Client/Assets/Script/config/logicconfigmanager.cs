using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class LogicConfigManager
{
   private static LogicConfigManager ConfigManager;

   public static LogicConfigManager Instance()
       {
           if (ConfigManager == null)
           {
               ConfigManager = new LogicConfigManager();
           }
           return ConfigManager;
       }



//构造函数必须实例化对象

   private LogicConfigManager()
   {
       //先把类给实例化
       server_mysql = new ServerConfig();
       mingcheng = new MingChengConfig();
   }


//初始化所有模块的配置

   public bool Init()
   {
       //读取游戏称号配置
       {
           if (!mingcheng.Init(@"Assets\Script\config\xml\mingcheng.xml"))
           {

               return false;
           }
       }

       //读取服务器与数据库配置
       {
           if (!server_mysql.Init(@"Assets\Script\config\xml\server_mysql.xml"))
           {
               return false;
           }
       }

       //如果还有其他模块的配置可以在这里扩展代码
      

       return true;
   }


    public MingChengConfig GetMingChengConfig(){return mingcheng;} 

	public ServerConfig GetServerMysqlConfig(){return server_mysql;}


   //以下是各个模块配置类的添加

    private ServerConfig server_mysql;

    private MingChengConfig mingcheng;   
    

    }

