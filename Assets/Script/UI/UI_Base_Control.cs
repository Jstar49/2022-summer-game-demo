using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base_Control : MonoBehaviour
{
    public UI_Souls uI_Souls;
    public UI_PlayerStatus uI_PlayerStatus;

    // 更新魂
    public void UpdateSouls(long nums){
        uI_Souls.UpdateSouls(nums);
    }

    // 更新血量
    public void UpdateWealth(int wealth){
        
    }
}
