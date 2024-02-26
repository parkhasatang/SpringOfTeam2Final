using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using DG.Tweening;

public class PlayerDamageUI : MonoBehaviour
{
    public TextMeshPro damageTxt;
    public float damage;
    private Vector3 originSize;
    private bool isAnimating = false;
    private Color originalColor;
    void Start()
    {
        originSize = damageTxt.transform.localScale;
        originalColor = damageTxt.color;
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
        Color chanageColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        // 애니메이션 실행
        yield return damageTxt.transform.DOMoveY(transform.position.y + 1.5f, 0.2f).SetEase(Ease.OutElastic);
        yield return damageTxt.transform.DOScale(originSize * 2, 0.2f).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return damageTxt.DOColor(chanageColor, 0.5f);
        //yield return damageTxt.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
        //yield return damageTxt.transform.DOScale(originSize, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();

        DOVirtual.DelayedCall(0.6f, () =>
        {
            damageTxt.color = originalColor;
            gameObject.SetActive(false);
            isAnimating = false; 
        });
    }

    //private void DamageSequence()
    //{
    //    DG.Tweening.Sequence sequence = DOTween.Sequence();

    //    sequence.Append(damageTxt.transform.DOMoveY(transform.position.y + 1.5f, 0.2f).SetEase(Ease.OutElastic);
    //}
}
