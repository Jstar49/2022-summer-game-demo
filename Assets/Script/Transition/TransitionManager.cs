
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // 初始 场景
    public string startSceneName = string.Empty;
    private CanvasGroup fadeCanvasGroup;
    private bool isFade;

    private void OnEnable() {
        EvenHandler.TransitionEvent += OnTransitionEvent;
    }
    private void OnDisable() {
        EvenHandler.TransitionEvent -= OnTransitionEvent;
    }

    private void Start() {
        StartCoroutine(LoadSceneSetActive(startSceneName));
        
        fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
    }

    private void OnTransitionEvent(string sceneToGo, Vector3 posToGo){
        if (!isFade){
            StartCoroutine(Transition(sceneToGo, posToGo));
        }
    }
    
    /**
     * @description: 加载新场景并刷新
     * @ 场景名，新场景的刷新位置
     * @return {*}
     */
    private IEnumerator Transition(string sceneName, Vector3 targetPosition){
        // 场景卸载之前
        EvenHandler.CallBeforeSceneUnloadEvent();
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        yield return LoadSceneSetActive(sceneName);

        EvenHandler.CallMoveToPosition(targetPosition);
        yield return Fade(0);
        // 加载场景之后
        // EvenHandler.CallAfterSceneloadEvent();
    }

    // 加载场景并设置 active
    private IEnumerator LoadSceneSetActive(string sceneName){
        yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount -1);
        SceneManager.SetActiveScene(newScene);

        EvenHandler.CallAfterSceneloadEvent();
    }

    /**
     * @description: 
     * @targetAlpha: 1 是黑，0是透明
     * @return {*}
     */
    private IEnumerator Fade(float targetAlpha){
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / 1.5f;

        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha)){
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = true;
    }
}
