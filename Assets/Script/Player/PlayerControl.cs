using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D player;
    public Collider2D coll;
    public Collider2D bodyColl;
    private Animator anim;
    [Header("移动参数")]
    public float moveSpeed;
    public float nowSpeed;
    // 敌人 layer
    // public LayerMask enemyLayer;
    //翻滚速度
    private float rollSpeed;
    private float xVelocity;

    [Header("跳跃参数")]
    public float jumpForce = 2f;

    // 在场景切换或者其他情况下玩家无法控制角色
    private bool inputDisable;

    public LayerMask ground;

    // public UI_PlayerStatus ui_PlayerStatus;
    [Header("UI控制基类")]
    public UI_Base_Control uI_Base_Control;
    [Header("数据控制基类")]
    public Data_Base_Control data_Base_Control;

    private void OnEnable() {
        EvenHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EvenHandler.AfterSceneloadEvent += OnAfterSceneUnloadEvent;
        EvenHandler.MoveToPosition += OnMoveToPosition;

    }
    private void OnDisable() {
        EvenHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EvenHandler.AfterSceneloadEvent -= OnAfterSceneUnloadEvent;
        EvenHandler.MoveToPosition -= OnMoveToPosition;
    }

    private void OnMoveToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    private void OnAfterSceneUnloadEvent()
    {
        inputDisable = false;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        inputDisable = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        nowSpeed = moveSpeed;
        rollSpeed = moveSpeed;
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // 血条与蓝条初始化
        // PlayerHealthChanged();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDisable == false){
            Jump();
            Roll();
            Attack();
            Block();
        }else{
            xVelocity = 0f;
        }
        
    }

    private void FixedUpdate() {
        if (inputDisable == false){
            
            PlayerMovement();
            
        }
        // 物理检测
        PhysicsCheck();
        SwitchAnim();
        
    }
    void PhysicsCheck(){
        // RaycastHit2D rightCheck = Raycast(new Vector2(0.2f, 0f), Vector2.right, 0.2f, enemyLayer);
        // RaycastHit2D leftCheck = Raycast(new Vector2(-0.2f, 0f), Vector2.left, 0.2f, enemyLayer);
        // if ()
    }
    // 移动
    public void PlayerMovement()
    {
        xVelocity = Input.GetAxis("Horizontal");
        // Debug.Log(xVelocity);
        FilpDirection();
        RaycastHit2D moveDirectEnemy = Raycast(new Vector2(xVelocity>0.1f?0.2f:-0.2f, 0f), 
                                            new Vector2(xVelocity, 0f), 
                                            0.5f, LayerMask.GetMask("Enemy"));
        if (!anim.GetBool("Rolling")){
            if (moveDirectEnemy){
                nowSpeed = 0f;
            }else{
                nowSpeed = moveSpeed;
            }
        }
        
        // 在某些时候不应该允许移动，比如防御的时候
        if (!anim.GetBool("Block")){
            // nowSpeed = moveSpeed;
            player.velocity = new Vector2(xVelocity * nowSpeed*Time.deltaTime*60, player.velocity.y);
            // anim.SetFloat("Running", Mathf.Abs(xVelocity));
        }
    }
    // 角色朝向翻转
    void FilpDirection()
    {
        if (xVelocity < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (xVelocity > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    // 格挡防御
    void Block(){
        if (Input.GetKeyDown(KeyCode.C) ){
            if (!anim.GetBool("Block")){
                anim.SetBool("Block", true);
                bodyColl.enabled = false;
                // 如果是在移动状态，停止移动
                if (anim.GetFloat("Running") > 0.1f){
                    player.velocity = new Vector2(0f * nowSpeed, player.velocity.y);
                    anim.SetFloat("Running", Mathf.Abs(0f));
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.C) ){
            gameObject.GetComponent<AttackJudge>().EndBlockJudge();
            anim.SetBool("Block", false);
            bodyColl.enabled = true;
        }
    }
    // 跳跃
    void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) ){
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            // jumpAudio.Play();
            anim.SetBool("Jumping", true);
            // anim.SetBool("Idle", false);
        }
    }
    // 翻滚
    void Roll(){
        if (Input.GetKeyDown(KeyCode.Z) ){
            if ((anim.GetFloat("Running")>0.1f || 
                anim.GetBool("Idle")) &&
                (!anim.GetBool("Jumping") && !anim.GetBool("Falling") && !anim.GetBool("Rolling"))){
                anim.SetBool("Rolling", true);
                Debug.Log(" set Rolling true");
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<BoxCollider2D>().enabled = false;
                if (anim.GetFloat("Running")>0.1f){
                    // player.AddForce(new Vector2(jumpForce,0f), ForceMode2D.Impulse);
                    nowSpeed = moveSpeed * 1.5f;
                    // nowSpeed = moveSpeed;
                }
                // anim.SetBool("Idle", false);
            }
            
        }
    }
    // 翻滚结束
    public void RollEnd(){
        anim.SetBool("Rolling", false);
        Debug.Log(" set Rolling false");
        gameObject.layer = LayerMask.NameToLayer("Player");
        GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<BoxCollider2D>().enabled = true;
        if (anim.GetFloat("Running")>0.1f){
            nowSpeed = moveSpeed;
        }
        
    }
    // 攻击判定
    void Attack(){
        if (Input.GetKeyDown(KeyCode.X) ){
            if ( !anim.GetBool("Rolling")){
                if(anim.GetBool("Attack2")){
                    anim.SetBool("Attack3", true);
                }
                if(anim.GetBool("Attack")){
                    anim.SetBool("Attack2", true);
                }
                anim.SetBool("Attack", true);
                
            }
            
            
        }
    }
    // 攻击结束
    void AttackEnd(){
        anim.SetBool("Attack", false);
    }
    void Attack2End(){
        anim.SetBool("Attack", false);
        anim.SetBool("Attack2", false);
    }
    void Attack3End(){
        anim.SetBool("Attack", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
    }
    //动画切换
    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if(player.velocity.y<0.1f && !coll.IsTouchingLayers(ground)){
            anim.SetBool("Falling",true);
        }
        if (anim.GetBool("Jumping")){
            if(player.velocity.y < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        else if (coll.IsTouchingLayers(ground)){
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }
        // 在某些时候不应该允许移动，比如防御的时候
        if (!anim.GetBool("Block")){
            // nowSpeed = moveSpeed;
            // player.velocity = new Vector2(xVelocity * nowSpeed*Time.deltaTime*60, player.velocity.y);
            anim.SetFloat("Running", Mathf.Abs(xVelocity));
        }
    }
    // 恢复蓝量
    public void GetBlue(int num){
        data_Base_Control.UpdateEnergy(num);
    }
    // 遭受伤害
    public void Hurted(int demage){
        Debug.Log("收到攻击");
        anim.SetBool("Hurt",true);
        // 生命值变化触发函数
        if (data_Base_Control.UpdateWealth(demage) == 0){
            anim.SetTrigger("Death");
        }
    }
    // 遭受伤害结束
    public void EndHurt(){
        anim.SetBool("Hurt",false);
    }
    // 死亡销毁人物
    public void DeadthDestoryPlayer(){
        Destroy(gameObject);
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

    // 获得硬币
    public void GetCoin(int value){
        data_Base_Control.UpdateSouls(value);
    }
}
