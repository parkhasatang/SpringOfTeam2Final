using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private Slider BgmSlider;
    [SerializeField] private Slider SFXSlider;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        BgmSlider.value = AudioManager.instance.bgmSource.volume;
        for (int i = 0; i < AudioManager.instance.sfxPlayers.Length; i++)
        {
            SFXSlider.value = AudioManager.instance.sfxPlayers[i].volume;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeBGMVolume()
    {
        AudioManager.instance.bgmSource.volume = BgmSlider.value;
    }

    public void OnChanageSFXVolume()
    {
        for (int i = 0; i < AudioManager.instance.sfxPlayers.Length; i++)
        {
            AudioManager.instance.sfxPlayers[i].volume = SFXSlider.value;
        }
    }
    public void OnOptionPanel()
    {
        OptionPanel.SetActive(true);
    }
    public void OffOptionPanel()
    {
        OptionPanel.SetActive(false);
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
