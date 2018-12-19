using UnityEngine;
using System.Collections;

//注：该类保存所有模块的Handler
//注：该C#文件挂载在unity全局可见的画布上,然后把各个模块的Handler的对象进行组件托拉赋值方式进行实例化

public class HandlerList : MonoBehaviour
    {
    /*
     private static HandlerList rList;

     public static HandlerList Instance()
        {
            if (rList == null)
            {
                rList = new HandlerList();
            }
            return rList;
        }

    */

     public IHandler GetHandler(CSMsgID uiMsgID)
     {
         //有多少个模块Handler就要添加多少个，handler规则都一样

         if (uiMsgID == CSMsgID.CS_MSGID_RegisterLogin)
             return m_oRegisterLoginHandler;
         else if (uiMsgID == CSMsgID.CS_MSGID_DecorateBAG)
             return m_oDecorateBagHandler;
         else if (uiMsgID == CSMsgID.CS_MSGID_BAG)
             return m_oBagHandler;
         //如果增加模块就在这里以下进行添加

         return null;
     }



     //把你模块的Handler类添加到这里,以下的对象不实例化，从unity拖动组件赋值到对应的handler模块对象上

     public LoginHandler m_oRegisterLoginHandler;               //登陆注册模块handler

     public BagHandler m_oBagHandler;                           //背包模块handler

     public DecorateBagHandler m_oDecorateBagHandler;          //装饰背包模块handler

     
    }
