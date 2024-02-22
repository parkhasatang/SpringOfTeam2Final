using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestShortCut : MonoBehaviour
{
    public TMP_Text questShortCutTxt;
    public Image questShortCutImg;
    public CanvasGroup questShortCutAlpha;

    private Quest quest;

    private void Awake()
    {
        quest = transform.parent.GetComponentInChildren<Quest>();
        quest.QuestStateChanged += OnQuestStateChanged;
    }

    public void OnQuestStateChanged()
    {
        ShortCutRefresh();
        ShortCutQuest();

    }

    public void ShortCutRefresh()
    {
        if (quest.isAccept)
        {
            ShortCutFadeIn();
        }
        else
        {
            ShortCutFadeOut();
        }
    }

    public void ShortCutFadeOut()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(questShortCutAlpha.DOFade(0f, 1f));

        // ½ÃÄö½º ½ÇÇà
        sequence.Play();
    }

    public void ShortCutFadeIn()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(questShortCutAlpha.DOFade(1f, 1f));

        sequence.Play();
    }

    public void ShortCutQuest()
    {
        questShortCutTxt.text = $"X {quest.requestCount}";
        questShortCutImg.sprite = quest.requsetImg.sprite;
    }
}
