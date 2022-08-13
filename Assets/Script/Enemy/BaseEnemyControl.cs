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
    // 攻击判定的距离
    public float canAttackLength;
    // 巡逻的位置
    public Transform leftPoint,rightPoint;
    // private RaycastHit2D canAttack;
    public float leftx,rightx;
    private bool faceLeft = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        nowSpeed = moveSpeed;
        attackCDTime = 0f;
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // 玩家位置
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        attackCDTime += Time.deltaTime;
    }
    private void FixedUpdate() {
        
        // 玩家在攻击范围内？
        RaycastHit2D canAttacks = Raycast(new Vector2(faceLeft?-1f:1f, 0f), 
                                    new Vector2(faceLeft?-1f:1f, 0f), canAttackLength, 
                                    LayerMask.GetMask("Player"));
        if (!canAttacks){
            // 移动
            GoblinMove();
            // 判断玩家是否在巡逻范围内
            GotoAttackPlayer();
        }else{
            // 攻击玩家行为
            // 停止移动
            nowSpeed = 0;
            body.velocity = new Vector2(0,0);
            anim.SetFloat("Runing",nowSpeed);
            AttackPlayer();
        }
    }
    public void AttackPlayer(){
        // 攻击间距
        if (attackCDTime <= 2.0f) return;
        base.Attack();
    }
    // 当玩家在巡逻范围时，追击玩家
    void GotoAttackPlayer(){
        if (!PlayerInMovePath()) return;
        anim.SetFloat("Runing",nowSpeed);
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
        nowSpeed = moveSpeed;
        anim.SetFloat("Runing",nowSpeed);
        // nowSpeed = moveSpeed;
        if (faceLeft){
            // 转向
            if (transform.position.x <= leftx){
                body.velocity = new Vector2(nowSpeed*Time.deltaTime*60, body.velocity.y);
                transform.localScale = new Vector3(1,1,1);
                faceLeft = false;
            }else{
                body.velocity = new Vector2(-nowSpeed*Time.deltaTime*60, body.velocity.y);
            }
        }else{
            // 转向
            if (transform.position.x >= rightx){
                body.velocity = new Vector2(-nowSpeed*Time.deltaTime*60, body.velocity.y);
                transform.localScale = new Vector3(-1,1,1);
                faceLeft = true;
            }else{
                body.velocity = new Vector2(nowSpeed*Time.deltaTime*60, body.velocity.y);
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
            body.AddForce(new Vector2(-nowSpeed,2f), ForceMode2D.Impulse);
        }
        else{
            // 玩家在左边，向右击飞
            body.AddForce(new Vector2(nowSpeed,2f), ForceMode2D.Impulse);
        }
        
    }
    // 射线
    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);
        return hit;
    }
}
