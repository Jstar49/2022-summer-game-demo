using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwithBounds : MonoBehaviour
{
    private void OnEnable() {
        EvenHandler.AfterSceneloadEvent += SwitchConfinerShape;
    }
    private void OnDisable() {
        EvenHandler.AfterSceneloadEvent -= SwitchConfinerShape;
    }

    //TODO: 切换场景后更改调用
    private void Start() {
        // SwitchConfinerShape();
    }
    // 游戏内场景边界
    public void SwitchConfinerShape()
    {
        // Debug.Log(GameObject.FindGameObjectWithTag("BoundsConfiner"));
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        
        confiner.m_BoundingShape2D = confinerShape;

        // 修改了碰撞边界
        confiner.InvalidatePathCache();
    }
}
