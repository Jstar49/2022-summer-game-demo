using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 定义防御判断以及防御行为
*/
public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 当与敌人的攻击 collider 碰撞时触发
    private void  OnTriggerEnter2D(Collider2D collision) {
        // Debug.Log("collision Judge!...");
        if (collision.gameObject.tag == "EnemyAttack"){
            Debug.Log("Touch Attack!...");
            // Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            // Debug.Log(enemy);
            // enemy.Attacked(attackDemage);
        }
    }
}
