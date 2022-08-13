using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 敌人的基类
*/
public class Enemy : MonoBehaviour
{
    // 普通攻击伤害值
    public int attackDemage;
    // 血量
    public int Wealth;
    // 移动速度
    public float moveSpeed;
    public float nowSpeed;
    // 动画对象
    protected Animator anim;
    // 死亡音效
    protected AudioSource deathAudio;
    public Rigidbody2D body;
    // 死亡奖励

    public GameObject deathReward;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // 收到攻击的事件
    public virtual void Attacked(int demage){
        // demage 为传过来的伤害值
        Wealth -= demage;
        if (Wealth <= 0){
            GetDeathReward();
            Deadth();
            
        }
        else{
            anim.SetBool("Hit",true);
        }
    }
    // 死亡后掉落奖励
    public void GetDeathReward(){
        deathReward.GetComponent<coin>().value = 3;
        Instantiate(deathReward,gameObject.transform.position,Quaternion.identity);
        Debug.Log(deathReward.GetComponent<coin>().value);
    }

    // 攻击
    public void Attack(){
        if (!anim.GetBool("Attack")){
            anim.SetBool("Attack",true);
        }
        
    }

    public void Deadth(){
        anim.SetTrigger("Death");
        // Destroy(gameObject);
    }

    protected virtual void DeathDestroyGameObj(){
        // Debug.Log("Destroy(gameObject)");
        Destroy(gameObject);
    }
    // 射线
    // public RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    // {
    //     Vector2 pos = transform.position;
    //     RaycastHit2D hit = Physics2D.Raycast(new Vector2(pos.x,0.5f) + offset, rayDiraction, length, layer);

    //     Color color = hit ? Color.red : Color.green;

    //     Debug.DrawRay(pos + offset, rayDiraction * length, color);
    //     return hit;
    // }
}
