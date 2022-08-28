using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EvenHandler
{
    public static event Action<string, Vector3> TransitionEvent;

    public static void CallTransitionEvent(string sceneName, Vector3 pos){
        TransitionEvent?.Invoke(sceneName, pos);
    }

    // 场景变化之前调用
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent(){
        BeforeSceneUnloadEvent?.Invoke();
    }

    // 场景变化之后调用
    public static event Action AfterSceneloadEvent;
    public static void CallAfterSceneloadEvent(){
        AfterSceneloadEvent?.Invoke();
    }

    public static event Action<Vector3> MoveToPosition;

    public static void CallMoveToPosition(Vector3 targetPosition){
        MoveToPosition?.Invoke(targetPosition);
    }
}
