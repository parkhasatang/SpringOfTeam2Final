using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarTest : MonoBehaviour
{
    public Image Hpbar;
    // Start is called before the first frame update
    void Start()
    {
        Hpbar.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
