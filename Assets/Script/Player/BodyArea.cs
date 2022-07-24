using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyArea : MonoBehaviour
{
    
    private void  OnTriggerEnter2D(Collider2D collision) {
        // Debug.Log("collision 触碰敌人!...");
        if (collision.gameObject.tag == "Enemy"){
            Debug.Log("触碰敌人");
            // gameObject.GetComponentInParent<PlayerControl>().nowSpeed = 0;
        }
    }
}
