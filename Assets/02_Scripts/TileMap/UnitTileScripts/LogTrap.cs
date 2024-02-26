using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTrap : MonoBehaviour
{
    public GameObject logTrap;
    private float damage = 10f;
    private HealthSystem healthSystem = null;
    private bool isRolling;
    private float positionY;
    // Start is called before the first frame update
    void Start()
    {
               
    }
    private void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (gameObject.activeSelf&&!isRolling)
    //    {
            
    //        StartCoroutine(PlayAnimation());
    //    }
    //}

    IEnumerator PlayAnimation()
    {
        isRolling = true;   
        yield return transform.DOMoveY(transform.position.y- 20f, 5f);

        DOVirtual.DelayedCall(4f, () =>
        {
            isRolling = false;
            logTrap.SetActive(false);
            
        });
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            healthSystem.ChangeHealth(-damage);
        }
    }
}
