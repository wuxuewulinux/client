using UnityEngine;
using System.Collections;

//登录注册模块


public class LoginHandler : MonoBehaviour ,IHandler{                        //每个模块都会继承IHandler接口

    public void MessageReceive(CSMsg model)                                 //封装接口函数
    {
        switch (model.Body.RegisterLoginRsp.Cmd)                            //访问模块的子功能
        {
            case CSRegisterLoginCmd.CSRegisterLoginCmd_Login:               // 登录模块          
                 OnLogin(model);                                            //登录模块要处理的逻辑
                 break;
            case CSRegisterLoginCmd.CSRegisterLoginCmd_Register:            //注册模块
                 OnRegister(model);                                        
                 break;
            case CSRegisterLoginCmd.CSRegisterLoginCmd_Quit:                 //退出模块               
                 OnQuit(model);                                                   
                 break;                                             
        }

        return;
        
    }

    //下面就是该模块各个功能的逻辑处理


    public void OnLogin(CSMsg rModel)
    {
        if (rModel.Body.RegisterLoginRsp.RspParam.LoginRsp.Type == 3)
        {
            //登录成功就要初始化role数据
            RoleData.Instance().SetDBRoleInfo(rModel.Body.RegisterLoginRsp.RspParam.LoginRsp.Role);

        }
        
        return;
    }

    public void OnRegister(CSMsg rModel)
    {
        
        return;
    }

    public void OnQuit(CSMsg rModel)
    {
        
        return;
    }


    //下面是一些unity的组件私有对象，根据项目需求去填充

}
