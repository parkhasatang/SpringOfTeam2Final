using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditor.Build.Content;
using UnityEngine.UIElements;

public class PlayerDamageUI : MonoBehaviour
{
    public TextMeshPro damageTxt;
    public float damage;
    private Vector3 originSize;
    private bool isAnimating = false; 

    void Start()
    {
        originSize = damageTxt.transform.localScale; 
    }

    void Update()
    {
        if (gameObject.activeSelf && !isAnimating) 
        {
            StartCoroutine(PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        isAnimating = true; 

        damageTxt.text = damage.ToString();

        // 애니메이션 실행
        yield return damageTxt.transform.DOMoveY(transform.position.y + 2f, 0.5f).SetEase(Ease.OutQuad);
        yield return damageTxt.transform.DOScale(originSize * 2, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return damageTxt.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return damageTxt.transform.DOScale(originSize, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();

        DOVirtual.DelayedCall(1f, () =>
        {
            gameObject.SetActive(false);
            isAnimating = false; 
        });
    }
}
