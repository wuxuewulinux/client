using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateBagHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(CSMsg model)                                 //封装接口函数
    {
        switch (model.Body.DecorateBagRsp.Cmd)                            //访问模块的子功能
        {
            case CSDecorateBagCmd.CSDecorateBagCmd_Fetch:               // 装饰背包界面显示功能          
                OnFetch(model);                                            
                break;
            case CSDecorateBagCmd.CSDecorateBagCmd_ShowSet:            //装饰背包显示设置物品装饰
                OnShowSet(model);
                break;
            case CSDecorateBagCmd.CSDecorateBagCmd_VipFetch:                 //VIP装饰背包界面展现功能               
                OnVipFetch(model);
                break;
        }

        return;
    }


    //下面就是该模块各个功能的逻辑处理

    public void OnFetch(CSMsg rModel)
     {


        return;
     }


    public void OnShowSet(CSMsg rModel)
    {


        return;
    }


    public void OnVipFetch(CSMsg rModel)
    {


        return;
    }


    //下面是一些unity的组件私有对象，根据项目需求去填充

}
