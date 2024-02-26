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
        SceneManager.LoadScene("MainScene");
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
