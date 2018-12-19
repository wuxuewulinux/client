using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ///////////////////////    所有模块的 数据结构类都在这里声明 /////////////////////

//注：(重点)每个类都有自己的 SetInfo函数

// static int DB_MAX_BAG_GRID_NUM = 100;


//......................................................背包模块数据..............................................................

public class tagDBBagGrid
{
	UInt32 m_uiID; // Item ID
	UInt32 m_uiNum; // Item Num

    public tagDBBagGrid()
    {
        m_uiID = 0;
        m_uiNum = 0;
    }
	public void SetInof(DBBagGrid msg)
    {
        if(msg.HasID)
        {
            m_uiID = msg.ID;
        }
        if(msg.HasNum)
        {
            m_uiNum = msg.Num;
        }
        return;
    }
} 

public class tagDBBagGridList
{
    public const int DB_MAX_BAG_GRID_NUM = 100;                             //物品ID最多只有100种
	public int m_nGridsRef;                                                 //人物拥有多少个物品ID
	public tagDBBagGrid[] m_astGrids;                                       //用一个数组来保存物品ID

    public tagDBBagGridList()
    {
        m_nGridsRef = 0;
        m_astGrids = new tagDBBagGrid[DB_MAX_BAG_GRID_NUM];
    }
	
	public void SetInfo(DBBagGridList msg)
    {
        m_nGridsRef = msg.GridsCount;
        for (int i = 0; i < msg.GridsCount; i++)
        {
            m_astGrids[i].SetInof(msg.GetGrids(i));
        }
         return;
    }

}


public class tagDBBagInfo             //背包模块用到的数据
{
    //该模块用到的数据在以下填写，数据必须是 public
    public tagDBBagGridList GridList;

    public tagDBBagInfo()
    {
     GridList = new tagDBBagGridList();
    }

    public void SetInfo(DBBagInfo rBag)       //每个类都有自己的 SetInfo 函数
    {
        if (rBag.HasGridList)
        {
            GridList.SetInfo(rBag.GridList);
        }
        
        return;
    }
}


public class BagManager
{
    public tagDBBagInfo BagInfo;

    public BagManager()
    {
        BagInfo = new tagDBBagInfo();
    }

    public void SetInfo(DBBagInfo rBagInfo)
    {
        if ((rBagInfo != null))
        {
            BagInfo.SetInfo(rBagInfo);
        }
    }

}

//......................................................背包模块数据..............................................................


//......................................................装饰背包数据..............................................................

public class tagDBDecorateItem
{
    UInt32 m_uiID; 
    UInt32 m_uiType;
    UInt64 m_uiEndTime;

    public tagDBDecorateItem()
    {
        m_uiID = 0;
        m_uiType = 0;
        m_uiEndTime = 0;
    }

    public void SetInfo(DBDecorateItem msg)
    {
        if (msg.HasId)
        {
            m_uiID = msg.Id;
        }
        if (msg.HasType)
        {
            m_uiType = msg.Type;
        }
        if (msg.HasEndTime)
        {
            m_uiEndTime = msg.EndTime;
        }
        return;
    }
}

public class tagDBDecorateItemList
{
    public const int DB_MAX_DECORATE_BAG_NUM = 100;                             
    public int m_nGridsRef;                                                 
    public tagDBDecorateItem[] m_astGrids;                                       

    public tagDBDecorateItemList()
    {
        m_nGridsRef = 0;
        m_astGrids = new tagDBDecorateItem[DB_MAX_DECORATE_BAG_NUM];
    }

    public void SetInfo(DBDecorateItemList msg)
    {
        m_nGridsRef = msg.DecorateGridsCount;
        for (int i = 0; i < msg.DecorateGridsCount; i++)
        {
            m_astGrids[i].SetInfo(msg.GetDecorateGrids(i));
        }
        return;
    }

}


public class tagDBDecorateBagInfo             //背包模块用到的数据
{
    //该模块用到的数据在以下填写，数据必须是 public
    public tagDBDecorateItemList GridList;

    public tagDBDecorateBagInfo()
    {
        GridList = new tagDBDecorateItemList();
    }

    public void SetInfo(DBDecorateBagInfo rBag)       //每个类都有自己的 SetInfo 函数
    {
        if (rBag.HasBagGridList)
        {
            GridList.SetInfo(rBag.BagGridList);
        }

        return;
    }
}


public class tagDBDecorateBagInfoList
{
    public const int DB_MAX_DECORATE_BAG = 30;
    public int m_nGridsRef;
    public tagDBDecorateBagInfo[] m_astDecorateBagList;

    public tagDBDecorateBagInfoList()
    {
        m_nGridsRef = 0;
        m_astDecorateBagList = new tagDBDecorateBagInfo[DB_MAX_DECORATE_BAG];
    }

    public void SetInfo(DBDecorateBagInfoList msg)
    {
        m_nGridsRef = msg.DecorateBagListCount;
        for (int i = 0; i < msg.DecorateBagListCount; i++)
        {
            m_astDecorateBagList[i].SetInfo(msg.GetDecorateBagList(i));
        }
        return;
    }

}


public class tagDBDecorateBagModuleInfo             //背包模块用到的数据
{
    //该模块用到的数据在以下填写，数据必须是 public
    public const int DB_MAX_DECORATE_BAG = 30;
    public tagDBDecorateBagInfoList BagList;
    UInt32[] m_astTypeList;

    public tagDBDecorateBagModuleInfo()
    {
         BagList = new tagDBDecorateBagInfoList();
         m_astTypeList = new UInt32[DB_MAX_DECORATE_BAG];
    }

    public void SetInfo(DBDecorateBagModuleInfo rBag)       //每个类都有自己的 SetInfo 函数
    {
        if (rBag.HasDecorateBagInfoList)
        {
            BagList.SetInfo(rBag.DecorateBagInfoList);
        }
        int Max = rBag.TypeListCount;
        for (int i = 0; i < Max; i++)
        {
            m_astTypeList[i] = rBag.GetTypeList(i);
        }
        return;
    }
}

///////////////

public class tagDBDecorateBagVIPItem
{
    UInt32 m_uiGrade; // Item ID
    UInt32 m_uiExper; // Item Num

    public tagDBDecorateBagVIPItem()
    {
        m_uiGrade = 0;
        m_uiExper = 0;
    }

    public void SetInof(DBDecorateBagVIPItem msg)
    {
        if (msg.HasGrade)
        {
            m_uiGrade = msg.Grade;
        }
        if (msg.HasExper)
        {
            m_uiExper = msg.Exper;
        }
        return;
    }
}



public class tagDBDecorateBagVIPList
{
    public const int DB_MAX_VIP = 3;                             
    public int m_nGridsRef;                                                 
    public tagDBDecorateBagVIPItem[] m_astVIPGrids;

    public tagDBDecorateBagVIPList()
    {
        m_nGridsRef = 0;
        m_astVIPGrids = new tagDBDecorateBagVIPItem[DB_MAX_VIP];
    }

    public void SetInfo(DBDecorateBagVIPList msg)
    {
        m_nGridsRef = msg.VIPItemListCount;
        for (int i = 0; i < msg.VIPItemListCount; i++)
        {
            m_astVIPGrids[i].SetInof(msg.GetVIPItemList(i));
        }
        return;
    }

}


public class tagDBDecorateBagVIPInfo            
{
    public tagDBDecorateBagVIPList VIPGridList;

    public tagDBDecorateBagVIPInfo()
    {
         VIPGridList = new tagDBDecorateBagVIPList();
    }

    public void SetInfo(DBDecorateBagVIPInfo rBag)
    {
        if (rBag.HasVIPListInfo)
        {
            VIPGridList.SetInfo(rBag.VIPListInfo);
        }

        return;
    }
}


public class DecorateBagManager
{
    public tagDBDecorateBagModuleInfo DecorateBagInfo;

    public tagDBDecorateBagVIPInfo VipBagInfo;

    public DecorateBagManager()
    {
         DecorateBagInfo = new tagDBDecorateBagModuleInfo();
         VipBagInfo = new tagDBDecorateBagVIPInfo();
    }

    public void SetInfo(DBDecorateBagModuleInfo rBagInfo)
    {
        if ((rBagInfo != null))
        {
            DecorateBagInfo.SetInfo(rBagInfo);
        }
    }

    public void SetVIPInfo(DBDecorateBagVIPInfo rVipBagInfo)
    {
        if ((rVipBagInfo != null))
        {
            VipBagInfo.SetInfo(rVipBagInfo);
        }
    }

}

//.....................................................装饰背包数据...............................................................




// ///////////////////////    所有模块的 数据结构类都在这里声明 /////////////////////










//注：RoleData类当用户登录成功就会进行实例化了，在登录成功功能中会进行实例化该类所有数据

public class RoleData
{


       private static RoleData rRole;

       private RoleData()
       {
           //所有模块的类结构必须在构造函数中实例化

           m_oBagMgr = new BagManager();                    //role实例化背包模块类

           m_oDecorateBagMgr = new DecorateBagManager();    //role实例化装饰背包模块类


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
           if (RoleInfo.HasDecorateBagModuleInfo)
           {
               m_oDecorateBagMgr.SetInfo(RoleInfo.DecorateBagModuleInfo);
           }
           if (RoleInfo.HasVIPInfo)
           {
               m_oDecorateBagMgr.SetVIPInfo(RoleInfo.VIPInfo);
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

    public DecorateBagManager m_oDecorateBagMgr;                    //装饰背包模块            


}

