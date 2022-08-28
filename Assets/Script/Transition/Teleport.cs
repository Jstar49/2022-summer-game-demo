using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string sceneToGo;
    public Vector3 positionToGo;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerBody")){
            // StartCoroutine(Transition(sceneToGo, positionToGo));
            EvenHandler.CallTransitionEvent(sceneToGo, positionToGo);
        }
    }
}
