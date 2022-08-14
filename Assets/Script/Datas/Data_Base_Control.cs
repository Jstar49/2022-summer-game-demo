using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data_Base_Control : MonoBehaviour
{
    // UI 管理
    public UI_Base_Control uI_Base_Control;
    public Data_Base_Souls data_Base_Souls;
    public Data_PlayerStatus data_PlayerStatus;


    private void Start() {
        // 游戏开始时更新血条和精力条与精力条
        UpdateMaxWealthAndMaxEnergy(0,0);
        UpdateWealth(0);
        UpdateEnergy(0);
        // 更新魂数量
        UpdateSouls(0);
        SaveData();
    }

    public void SaveData(){
        data_Base_Souls.SaveData();
        data_PlayerStatus.SaveData();
    }

    // 更新魂
    public void UpdateSouls(long nums) {
        // 更新数量
        data_Base_Souls.updateSouls(nums);
        // 更新 UI
        uI_Base_Control.UpdateSouls(data_Base_Souls.RetSoulsNum());
    }
    // 获取魂数量
    public long GetSoulsData(){
        return data_Base_Souls.RetSoulsNum();
    }
    // 更新生命值
    public int UpdateWealth(int nums){
        int ret = data_PlayerStatus.UpdateWealth(nums);
        uI_Base_Control.UpdateWealth(data_PlayerStatus.RetWealth());
        return ret;
    }
    // 增加精力值
    public void UpdateEnergy(int nums){
        data_PlayerStatus.UpdateEnergy(nums);
        uI_Base_Control.UpdateEnergy(data_PlayerStatus.RetEnergy());
    }
    // 更新最大生命值
    public void UpdateMaxWealthAndMaxEnergy(int wealth, int energy){
        data_PlayerStatus.UpdateMaxWealthAndMaxEnergy(wealth, energy);
        uI_Base_Control.UpdateMaxWealth(data_PlayerStatus.RetMaxWealth());
        uI_Base_Control.UpdateMaxEnergy(data_PlayerStatus.RetMaxEnergy());
    }
}
