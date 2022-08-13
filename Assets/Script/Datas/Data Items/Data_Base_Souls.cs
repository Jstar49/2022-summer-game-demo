using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
@ 魂（游戏内消耗品）基类管理
*/
[CreateAssetMenu(fileName = "Data_Souls", menuName = "Data/Data_Souls", order = 0)]
public class Data_Base_Souls : ScriptableObject
{
     public long soulNum;

     public void updateSouls(long nums){
        soulNum += nums;
     }
}
