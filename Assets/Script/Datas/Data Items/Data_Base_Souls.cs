using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
   //   返回魂数量
   public long RetSoulsNum(){
      return soulNum;
   }
   // 保存数据
   public void SaveData(){
      string filePath = Application.dataPath+"/Data/data_Souls.json";
      Debug.Log(filePath);
      
      string jsonInfo = JsonUtility.ToJson(this);
      // jsonInfo += JsonUtility.ToJson(data_Base_Souls);
      Debug.Log(jsonInfo);
      File.WriteAllText(filePath, jsonInfo);
   }
   // 读取数据
}
