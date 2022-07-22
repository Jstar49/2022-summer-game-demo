using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 敌人控制基础脚本
*/
public class BaseEnemyControl : Enemy
{
    // 攻击判定的 collider2D 对象
    public Collider2D attackColl;
    // 攻击的 CD 时间
    public float attackCDTime;
    // 玩家位置
    public Transform playerTrans;
    // 巡逻的位置
    public Transform leftPoint,rightPoint;
    public float leftx,rightx;
    private bool faceLeft = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackCDTime = 0f;
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        // 移动
        GoblinMove();
        // 判断玩家是否在巡逻范围内
        GotoAttackPlayer();
        // 攻击玩家行为
        AttackPlayer();
        
    }
    public void AttackPlayer(){
        attackCDTime += Time.deltaTime;
        // 敌人与玩家的距离
        Vector3 distance = transform.position - playerTrans.position;
        if (attackCDTime <= 2.0f) return;
        if (faceLeft){
            if (distance.x <= 2f && distance.x >=0){
                base.Attack();
                // attackCDTime = 0f;
            }
        }
        else{
            if (distance.x >= -2f && distance.x <=0){
                base.Attack();
                // attackCDTime = 0f;
            }
        }
    }
    // 当玩家在巡逻范围时，追击玩家
    void GotoAttackPlayer(){
        if (!PlayerInMovePath()) return;
        anim.SetFloat("Runing",moveSpeed);
        // 在玩家左边
        if (playerTrans.position.x < transform.position.x){
            // faceLeft = true;
            if(!faceLeft){
                transform.localScale = new Vector3(-1,1,1);
                faceLeft = true;
            }
        }
        else{
            // faceLeft = false;
            if(faceLeft){
                transform.localScale = new Vector3(1,1,1);
                faceLeft = false;
            }
        }
    }
    // 判断玩家是否在巡逻范围内
    bool PlayerInMovePath(){
        if (playerTrans.position.x <= rightx && playerTrans.position.x >= leftx){
            return true;
        }
        return false;
    }
    // 巡逻移动
    public void GoblinMove(){
        // if (PlayerInMovePath()) return;
        anim.SetFloat("Runing",moveSpeed);
        
        if (faceLeft){
            if (transform.position.x <= leftx){
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
                transform.localScale = new Vector3(1,1,1);
                faceLeft = false;
            }else{
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
            }
        }else{
            if (transform.position.x >= rightx){
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
                transform.localScale = new Vector3(-1,1,1);
                faceLeft = true;
            }else{
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
            }
        }
    }
    
    // 摧毁对象
    protected override void DeathDestroyGameObj(){
        Debug.Log("Destroy(gameObject)");
        Destroy(gameObject);
    }
    // 结束攻击
    public void EndHit(){
        anim.SetBool("Hit",false);
        
    }
    // 开始攻击判定
    void BeginAttack1Judge(){
        attackColl.enabled = true;
    }
    // 结束攻击判定
    void EndAttack1Judge(){
        attackColl.enabled = false;
        anim.SetBool("Attack", false);
        attackCDTime = 0f;
    }
    // 收到攻击的事件
    public override void Attacked(int demage){
        base.Attacked(demage);
        Debug.Log("遭受攻击");
        // 遭受攻击后击飞
        if (playerTrans.position.x > transform.position.x){
            // 玩家在右边，向左击飞
            body.AddForce(new Vector2(-moveSpeed,2f), ForceMode2D.Impulse);
        }
        else{
            // 玩家在左边，向右击飞
            body.AddForce(new Vector2(moveSpeed,2f), ForceMode2D.Impulse);
        }
        
    }
}
