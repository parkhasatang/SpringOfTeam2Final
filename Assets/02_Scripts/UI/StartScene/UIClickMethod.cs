using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIClickMethod : MonoBehaviour
{
    public Button startBtn;
    public Button exitBtn;
    
    public void ClickStartBtn()
    {
        StartCoroutine(LoadSceneAsync("MainScene"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // 로딩 프로세스를 나타내는 AsyncOperation 객체 생성
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // 씬을 활성화할 때까지 기다림
        asyncOperation.allowSceneActivation = false;

        // 로드가 완료되었는지 확인
        while (!asyncOperation.isDone)
        {
            // 필요한 경우 로딩 진행률 표시
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // 씬이 완전히 로드될 때의 완료 값은 0.9입니다.

            Debug.Log("로딩 진행률: " + (progress * 100) + "%");

            // 로드가 거의 완료되었는지 확인
            if (asyncOperation.progress >= 0.9f)
            {
                // 거의 완료되었다면 씬을 활성화할 수 있습니다.
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void ClickExitBtn()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
