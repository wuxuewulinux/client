using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//注：该文件挂载在全局可见的画布上

public class WarrningManager : MonoBehaviour {

    public static List<WarrningModel> errors = new List<WarrningModel>();

    [SerializeField]
    private WarrningWindow window;              //把错误弹框UI赋值给该变量
	void Update () {
        if (errors.Count > 0) {
            WarrningModel err = errors[0];
            errors.RemoveAt(0);
            window.active(err);
        }
        
	}
}
