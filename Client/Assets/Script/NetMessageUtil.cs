using UnityEngine;
using System.Collections;

//注：该C#文件挂载在unity全局可见的画布上。

public class NetMessageUtil : MonoBehaviour {
    
    private HandlerList rHadlerList;
	
	void Start () {

        rHadlerList = GetComponent<HandlerList>();              //获取HandlerList对象
	}
	


	void Update () {
        //每一帧查看是否有消息需要处理
        while (NetIO.Instance.messages.Count > 0) {
        CSMsg model = NetIO.Instance.messages[0];
            StartCoroutine("MessageReceive", model);                //创建一个携程，相当于一个线程
            NetIO.Instance.messages.RemoveAt(0);
        }
	    
	}

    void MessageReceive(CSMsg model) {

        IHandler rHandler = rHadlerList.GetHandler(model.Head.MsgID);   //根据模块号去调用对应的模块功能
        rHandler.MessageReceive(model);                                 //跳转到逻辑功能上
       
    }
}

