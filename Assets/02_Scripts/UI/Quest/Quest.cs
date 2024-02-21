using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    private Item requestItem;
    private Item rewardItem;
    private int requestCount;
    private int rewardCount;

    public Image requsetImg;
    public Image rewardImg;
    public TMP_Text requestCountTxt;
    public TMP_Text rewardCountTxt;

    public Button rewardButton;
    public Sprite activeBtnSprite;
    public Sprite defaultBtnSprite;
    public CanvasGroup canvasGroup;
    private Sequence sequence;

    private Inventory inventory;
    private ItemManager itemManager;

    private void Awake()
    {
        inventory = UIManager.Instance.playerInventoryData;
        itemManager = ItemManager.instance;
    }

    private void OnEnable()
    {
        ResetQuest();
    }

    // 랜덤 퀘스트 주기.
    public void CreateRandomQuest()
    {
        requestItem = itemManager.CreateRandomItemByType(2);
        requestCount = Random.Range(1, 21);
        rewardItem = itemManager.CreateRandomItemByType(8);
        rewardCount = Random.Range(1, 21);
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

        FadeOut();        
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
        ResetQuest();

        sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(1f, 1f));

        sequence.Play();
    }

    public void ResetQuest()
    {
        CreateRandomQuest();
        SetQuestImgAndTxt();
        CheckRequst();
    }
}
