using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyManager : Singleton<AnyManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        Show("UIPopup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string canvas)
    {
        GameObject uiPopup = Resources.Load<GameObject>($"UI/{canvas}");
        Instantiate(uiPopup);
    }
}
