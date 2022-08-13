using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Base_Control : MonoBehaviour
{
    // UI 管理
    public UI_Base_Control uI_Base_Control;
    public Data_Base_Souls data_Base_Souls;
    public Data_PlayerStatus data_PlayerStatus;


    private void Start() {
        // data_Base_Souls = Dictionary.
    }

    // 更新魂
    public void UpdateSouls(long nums) {
        // 更新数量
        data_Base_Souls.updateSouls(nums);
        // 更新 UI
        uI_Base_Control.UpdateSouls(data_Base_Souls.soulNum);
    }
    // 获取魂数量
    public long GetSoulsData(){
        return data_Base_Souls.soulNum;
    }
    // 更新生命值
    public int UpdateWealth(int nums){
        return data_PlayerStatus.UpdateWealth(nums);
    }
    // 增加蓝量
    public void UpdateEnergy(int nums){
        data_PlayerStatus.UpdateEnergy(nums);
    }
}
