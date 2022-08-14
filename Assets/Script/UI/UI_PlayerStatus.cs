using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_PlayerStatus : MonoBehaviour
{
    public Image maxRed;
    public Image red;
    public Image maxBlue;
    public Image blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 血条最大值变化
    public void MaxWealthChanged(int maxWealth){
        maxRed.rectTransform.sizeDelta = new Vector2(maxWealth*20, 25);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    // 血条变化 
    public void WeathChanged(int health){
        red.rectTransform.sizeDelta = new Vector2(health*20, 25);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    // 蓝条最大值变化
    public void MaxBlueChanged(int maxEnergy){
        maxBlue.rectTransform.sizeDelta = new Vector2(maxEnergy*20, 20);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    // 蓝条变化
    public void BlueChanged(int blueNum){
        blue.rectTransform.sizeDelta = new Vector2(blueNum*20, 20);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
