using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public event Action QuestStateChanged;

    private Item requestItem;
    private Item rewardItem;
    internal int requestCount;
    internal int rewardCount;

    public Image requsetImg;
    public Image rewardImg;
    public TMP_Text requestCountTxt;
    public TMP_Text rewardCountTxt;

    public Button acceptBtn;
    public Button resetBtn;
    public CanvasGroup canvasGroup;
    private Sequence sequence;

    private ItemManager itemManager;

    private void Start()
    {
        itemManager = ItemManager.instance;

        acceptBtn.onClick.AddListener(PressAcceptBtn);
    }

    // 랜덤 퀘스트 주기.
    public void CreateRandomQuest()
    {
        requestItem = itemManager.CreateRandomItemByType(2);
        requestCount = UnityEngine.Random.Range(1, 21);
        rewardItem = itemManager.CreateRandomItemByType(8);
        rewardCount = UnityEngine.Random.Range(1, 21);
    }

    // 퀘스트 필요 아이템과 보상의 이미지 결정.
    public void SetQuestImgAndTxt()
    {
        requsetImg.sprite = itemManager.GetSpriteByItemCode(requestItem.ItemCode);
        requestCountTxt.text = $"{requestCount}";
        rewardImg.sprite = itemManager.GetSpriteByItemCode(rewardItem.ItemCode);
        rewardCountTxt.text = $"{rewardCount}";
    }

    private void FadeOut()
    {
        sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(0f, 1f)).OnComplete(FadeIn);
        
        // 시퀀스 실행
        sequence.Play();
    }

    private void FadeIn()
    {
        sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(1f, 1f));

        sequence.Play();
    }

    public void ResetQuest()
    {
        CreateRandomQuest();
        Invoke("SetQuestImgAndTxt", 1f);

        FadeOut();
    }

    public void PressAcceptBtn()
    {
        SetQuestInfo();

        ResetQuest();
    }

    public void PressResetBtn()
    {
        ResetQuest();
    }

    private void SetQuestInfo()
    {
        QuestManager.instance.questInfo = new QuestInfo();

        QuestManager.instance.questInfo.requestItem = requestItem;
        QuestManager.instance.questInfo.requestCount = requestCount;

        QuestManager.instance.questInfo.rewardItem = rewardItem;
        QuestManager.instance.questInfo.rewardCount = rewardCount;
    }
}
