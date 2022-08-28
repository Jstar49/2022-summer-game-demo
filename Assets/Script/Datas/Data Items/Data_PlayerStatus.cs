using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Data_PlayerStatus", menuName = "Data/Data_PlayerStatus", order = 1)]
public class Data_PlayerStatus : ScriptableObject
{
    // 最大生命值
    public int maxWealth;
    // 当前生命值
    public int wealth;
    // 最大精力值
    public int maxEnergy;    
    // 当前精力值
    public int energy;
    
    // 保存数据
    public void SaveData(){
        string filePath = Application.dataPath+"/Data/data_PlayerStatus.json";
        Debug.Log(filePath);

        string jsonInfo = JsonUtility.ToJson(this);
        // jsonInfo += JsonUtility.ToJson(data_Base_Souls);
        Debug.Log(jsonInfo);
        File.WriteAllText(filePath, jsonInfo);
    }
    // 读取数据
    public void LoadData(){
      string filePath = Application.dataPath+"/Data/data_PlayerStatus.json";
      string jsonInfo = File.ReadAllText(filePath);
      JsonUtility.FromJsonOverwrite(jsonInfo,this);
    }
    // 更新生命值
    public int UpdateWealth(int nums){
        if (wealth + nums >= maxWealth){
            wealth = maxWealth;
        }else if(wealth + nums <=0){
            // 死亡返回0
            wealth = 0;
            return 0;
        }else{
            wealth += nums;
        }
        return 1;
    }
    // 返回生命值
    public int RetWealth(){
        return wealth;
    }
    // 更新精力
    public void UpdateEnergy(int nums){
        if (energy + nums >= maxEnergy){
            energy = maxEnergy;
        }else if (energy + nums <= 0){
            energy = 0;
        }else {
            energy += nums;
        }
    }
    // 返回精力值
    public int RetEnergy(){
        return energy;
    }

    // 更新最大生命值
    public void UpdateMaxWealthAndMaxEnergy(int wealth, int energy){
        maxWealth += wealth;
        maxEnergy += energy;
    }
    // 返回最大生命值和最大精力值
    public int RetMaxWealth(){
        return maxWealth;
    }
    public int RetMaxEnergy(){
        return maxEnergy;
    }
}
