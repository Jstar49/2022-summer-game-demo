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
        uI_PlayerStatus.WeathChanged(wealth);
    }
    // 更新精力
    public void UpdateEnergy(int energy) {
        uI_PlayerStatus.BlueChanged(energy);
    }
    // 最大血量的UI更新
    public void UpdateMaxWealth(int wealth){
        uI_PlayerStatus.MaxWealthChanged(wealth);
    }
    // 最大精力值的UI更新
    public void UpdateMaxEnergy(int energy){
        uI_PlayerStatus.MaxBlueChanged(energy);
    }
}
