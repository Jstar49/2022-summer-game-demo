using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
public class coin : MonoBehaviour
{
    // 价值
    public int value;

    void Update() {
        
    }
    private void FixedUpdate() {
        RaycastHit2D coinhit = Raycast(new Vector2(-0.3f, 0f), new Vector2(0.6f, 0f), 1f, LayerMask.GetMask("Player"));
        if (coinhit){
            coinhit.collider.gameObject.GetComponent<PlayerControl>().GetCoin(value);
            Destroy(gameObject);
        }
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.tag == "Player"){
    //         Debug.Log("硬币");
    //         other.gameObject.GetComponent<PlayerControl>().GetCoin(value);
    //         // Destroy(gameObject);
    //     }
    // }

    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);
        return hit;
    }

}
