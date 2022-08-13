using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_PlayerStatus", menuName = "Data/Data_PlayerStatus", order = 1)]
public class Data_PlayerStatus : ScriptableObject
{
    // 最大生命值
    public int maxWealth;
    // 当前生命值
    public int wealth;
    // 最大精力值
    public int maxEnergy;    // 当前精力值
    public int energy;
    
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
    // 更新精力
    public void UpdateEnergy(int nums){
        if (energy + nums >= maxEnergy){
            energy = maxEnergy;
        }else {
            energy += nums;
        }
    }

}
