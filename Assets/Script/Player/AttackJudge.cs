using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 该文件为玩家的攻击帧判定
* 也包括遭受攻击
*/
public class AttackJudge : MonoBehaviour
{
    // 攻击判定的 collider2D 对象
    public Collider2D attack1Coll;
    // 防御判定的 collider2D 对象
    public Collider2D BlockColl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 开始攻击判定
    void BeginAttack1Judge(){
        attack1Coll.enabled = true;
    }
    // 结束攻击判定
    void EndAttack1Judge(){
        attack1Coll.enabled = false;
    }
    
    // 开始防御判定
    void BeginBlockJudge(){
        BlockColl.enabled = true;
    }
    // 结束防御判定
    public void EndBlockJudge(){
        BlockColl.enabled = false;
    }
}
