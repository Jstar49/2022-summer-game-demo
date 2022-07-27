using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tools{
    class Tools{
        public RaycastHit2D Raycast(Transform transform, Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);
        return hit;
    }
}
}
// Update is called once per frame


