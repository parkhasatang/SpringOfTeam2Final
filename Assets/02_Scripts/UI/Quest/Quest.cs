using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IObservable
{
    event Action QuestStateChanged;
}

public class Quest : MonoBehaviour, IObservable
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
    public TMP_Text questResetTimerTxt;

    internal bool isAccept;
    private float resetTimer;
    public float ResetTimer
    {
        get { return resetTimer; }
        set
        {
            resetTimer = value;
            QuestResetTimer();
        }
    }

    public Button acceptBtn;
    public Button rewardButton;
    public Sprite activeBtnSprite;
    public Sprite defaultBtnSprite;
    public CanvasGroup canvasGroup;
    private Sequence sequence;

    private Inventory inventory;
    private ItemManager itemManager;

    private void Start()
    {
        inventory = UIManager.Instance.playerInventoryData;
        itemManager = ItemManager.instance;

        acceptBtn.onClick.AddListener(AcceptQuest);
        rewardButton.onClick.AddListener(GetReward);
    }

    private void Update()
    {
        if (!isAccept)
        {
            ResetTimer -= Time.deltaTime;

            if (ResetTimer <= 0f)
            {
                ResetQuest();
                ResetTimer = 10f;
            }
        }
        else
        {
            ResetTimer = 10f;
        }
    }

    private void OnQuestStateChanged()
    {
        QuestStateChanged?.Invoke();
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

    // 퀘스트 조건을 충족했는지 확인
    public void CheckRequst()
    {
        if (inventory.CheckStackAmount(requestItem.ItemCode, requestCount))
        {
            // 버튼 이미지 바꾸고 눌리게 해주기.
            rewardButton.interactable = true;
            rewardButton.image.sprite = activeBtnSprite;
        }
        else
        {
            rewardButton.interactable = false;
            rewardButton.image.sprite = defaultBtnSprite;
        }
    }

    // 보상 버튼을 누르면.
    public void GetReward()
    {
        inventory.RemoveItemFromInventory(requestItem.ItemCode, requestCount);
        inventory.GiveItemToEmptyInv(rewardItem, rewardCount);

        isAccept = false;

        ResetQuest();
        OnQuestStateChanged();
    }

    public void FadeOut()
    {
        sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(0f, 1f)).OnComplete(FadeIn);
        
        // 시퀀스 실행
        sequence.Play();
    }

    public void FadeIn()
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

    public void AcceptQuest()
    {
        isAccept = true;
        
        // 퀘스트 숏컷 상태 바꿔주기. 이벤트로 구현함.
        OnQuestStateChanged();

        CheckRequst();
    }

    public void QuestResetTimer()
    {
        questResetTimerTxt.text = $"{(int)ResetTimer}";
    }
}
