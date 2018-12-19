using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(CSMsg model)                                 //封装接口函数
    {
        switch (model.Body.BagRsp.Cmd)                                      //访问模块的子功能
        {
            case CSBagCmd.CSBagCmd_Fetch:                                   //背包界面显示功能          
                OnFetch(model);
                break;
            case CSBagCmd.CSBagCmd_Use:                                     //背包物品的使用功能
                OnBagUse(model);
                break;
        }

        return;
    }


    //下面就是该模块各个功能的逻辑处理

    public void OnFetch(CSMsg rModel)
    {


        return;
    }

    public void OnBagUse(CSMsg rModel)
    {


        return;
    }


    //下面是一些unity的组件私有对象，根据项目需求去填充

}
