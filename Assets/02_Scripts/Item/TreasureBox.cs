using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private Animator boxAnimator;
    // Start is called before the first frame update
    void Start()
    {
        boxAnimator = GetComponent<Animator>();
    }

    public void OpenTreasureBox()
    {
        boxAnimator.SetBool("Open", true);
        ItemManager.instance.itemPool.ItemSpawn(1002, gameObject.transform.position);
        // 2ÃÊµÚ¿¡ ÆÄ±«
        Destroy(gameObject, 2f);
    }
    
}
