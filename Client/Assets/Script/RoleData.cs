using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ///////////////////////    所有模块的 数据结构类都在这里声明 /////////////////////

//注：(重点)每个类都有自己的 SetInfo函数


public class BagManager             //背包模块用到的数据
{
    //该模块用到的数据在以下填写，数据必须是 public


    public void SetInfo(DBBagInfo rBag)       //每个类都有自己的 SetInfo 函数
    {
        return;
    }
}






// ///////////////////////    所有模块的 数据结构类都在这里声明 /////////////////////











//注：RoleData类当用户登录成功就会进行实例化了，在登录成功功能中会进行实例化该类所有数据

public class RoleData
{


       private static RoleData rRole;

       private RoleData()
       {
           //所有模块的类结构必须在构造函数中实例化

           m_oBagMgr = new BagManager();            //role实例化背包模块类


       }



       public static RoleData Instance()
       {
           if (rRole == null)
           {
               rRole = new RoleData();
           }
           return rRole;
       }


       public void SetDBRoleInfo(DBRoleInfo RoleInfo)           //初始化所有role的数据
       {
           if (RoleInfo.HasUID)
           {
               uUID = RoleInfo.UID;
           }
           if (RoleInfo.HasName)
           {
               sName = RoleInfo.Name;
           }
           if (RoleInfo.HasLevel)
           {
               uiLevel = RoleInfo.Level;
           }
           if (RoleInfo.HasRank)
           {
               uiRank = RoleInfo.Rank;
           }
           if (RoleInfo.HasLevelExper)
           {
               uiLevelExper = RoleInfo.LevelExper;
           }
           if (RoleInfo.HasRankExper)
           {
               uiRankExper = RoleInfo.RankExper;
           }

           //以下是各个模块类直接设置,有多少个模块就要设置多少个

           if (RoleInfo.HasBagInfo)              
           {
               m_oBagMgr.SetInfo(RoleInfo.BagInfo);             //设置背包模块所有数据
           }


           return;
       }



       //设置和获取role的数据

       public ulong GetUid() { return uUID; }

       public void SetName(string name) { sName = name; }
       public string GetName() { return sName; }

       public uint GetLevel() { return uiLevel; }
       public void SetLevel(uint level) { uiLevel = level; }

       public uint GetRank() { return uiRank; }
       public void SetRank(uint rank) { uiRank = rank; }

       public uint GetRankExper() { return uiRankExper; }
       public void SetRankExper(uint rank_exper) { uiRankExper = rank_exper; }

       public uint GetLevelExper() { return uiLevelExper; }
       public void SetLevelExper(uint level_exper) { uiLevelExper = level_exper; }


       
    //以下是role的所有用户的数据

    private ulong uUID;											//唯一uid

	private string sName;										//游戏名称

	private uint uiLevel;										//游戏等级

	private uint uiRank;										//段位级别

	private uint uiRankExper;									//该段位经验

	private uint uiLevelExper;									//人物等级经验

	//添加各个模块的类对象在这里,做成一个共有访问,模块就没必要用 在封装获取接口了。直接在外部调用就可以了

	public BagManager	m_oBagMgr;									//背包模块


}

