using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Souls : MonoBehaviour
{
    public TextMeshProUGUI soulNum;
    public Data_Base_Control data_Base_Control;
    // Start is called before the first frame update
    void Start()
    {
        // UpdateSouls();
    }
    public void UpdateSouls(long nums) {
        soulNum.text = nums.ToString();
        // GameObject.Find("")
    }

    
}
