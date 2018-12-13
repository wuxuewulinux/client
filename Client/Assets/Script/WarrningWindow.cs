using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//注：该文件挂载在一个错误弹框UI界面上

public class WarrningWindow : MonoBehaviour {

    [SerializeField]
    private Text text;

    WarrningResult result;

    public void active(WarrningModel value) {
        text.text = value.value;
        this.result = value.result;
        gameObject.SetActive(true);
    }

    public void close() {
        gameObject.SetActive(false);
        if (result != null) {
            result();
        }
        
    }
}
