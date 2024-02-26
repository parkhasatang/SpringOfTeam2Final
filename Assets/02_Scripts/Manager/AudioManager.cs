using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    

    [Header("#BGM")]
    public  AudioClip BGMclip;
    public float bgmVolume;
    public AudioSource bgmSource;
    public float bgmChangeVolume;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public float sfxChangerVolume;
    public int channels;
    private int channelIndex;
    public AudioSource[] sfxPlayers;

    public enum Sfx
    {
        PlayerAttack, PlayerDamage, PlayerEquip,
        PlayerEat, PlayerHarvest, PlayerPulling,
        MonsterDamage, ItemCrafting, MakeFood, PlayerMining
    }


    private void Awake()
    {
        instance = this;

        Init();
        PlayBgm();
    }

    private void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = true;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmChangeVolume = bgmSource.volume;
        bgmSource.clip = BGMclip;

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
       
        
        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxChangerVolume = sfxPlayers[i].volume;
        }
    }
    public void PlayBgm()
    {
        bgmSource.Play();
    }

    public void PlaySffx(Sfx sfx)
    {
        for(int index = 0; index < sfxPlayers.Length;index++)
        {
            int loopindex = (index + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopindex].isPlaying)
                continue;

            channelIndex = loopindex;
            sfxPlayers[loopindex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopindex].Play();
            break;
        }        
    }
}
