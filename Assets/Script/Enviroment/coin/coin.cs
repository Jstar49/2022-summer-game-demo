using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class coin : MonoBehaviour
{
    // 价值
    public int value;
    private Transform playerTrans;
    // 初始位置
    private Vector2 startPos;
    // 中间位置
    private Vector2 midPos;
    // 目标位置
    private Vector2 targetPos;
    private float percentSpeed;
    private float percent;
    public float moveSpeed;
    private bool canMove = false;
    private bool arrived = false;
    private void Start() {
        Invoke("DeleyTimes",1);
        
        // initialized = true;
        
    }
    void DeleyTimes(){
        canMove = true;
        // 以下为贝塞尔曲线相关代码
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
        targetPos = playerTrans.position;
        midPos = GetMidPos(startPos,targetPos);
        percentSpeed = moveSpeed / (targetPos - startPos).magnitude;
        percent = 0f;
    }
    Vector2 GetMidPos(Vector2 a, Vector2 b){
        Vector2 m = Vector2.Lerp(a,b,0.1f);
        Vector2 normal = Vector2.Perpendicular(a-b).normalized;
        float rd = Random.Range(-2f,2f);
        float curveRatio = 0.3f;
        return m+(a-b).magnitude * curveRatio*rd*normal;
    }

    void Update() {
        if (canMove){
            // 贝塞尔曲线实现
            // percent += percentSpeed * Time.deltaTime;
            // if (percent > 1)
            //     percent = 1;
            // transform.position = Tools.Bezier(percent,startPos,midPos,targetPos);
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * moveSpeed);
        }
        
    }
    private void FixedUpdate() {
        RaycastHit2D coinhit = Raycast(new Vector2(-0.3f, 0f), new Vector2(0.6f, 0f), 1f, LayerMask.GetMask("Player"));
        if (coinhit){
            coinhit.collider.gameObject.GetComponent<PlayerControl>().GetCoin(value);
            Debug.Log(value);
            Destroy(gameObject);
        }
        // if (canMove){
            // transform.Translate(GameObject.FindGameObjectWithTag("Player").transform.position);
        // }
    }


    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);
        return hit;
    }

}
