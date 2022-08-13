using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools{
    public RaycastHit2D Raycast(Transform transform, Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);
        return hit;
    }

    // 贝塞尔曲线
    public static Vector2 Bezier(float t, Vector2 a, Vector2 b, Vector2 c){
        var ab = Vector2.Lerp(a,b,t);
        var bc = Vector2.Lerp(b,c,t);
        return Vector2.Lerp(ab,bc,t);
    }
}

// Update is called once per frame


