using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleTrap : MonoBehaviour
{
    private bool animationState = false;
    private float damage = 30f;
    private HealthSystem healthSystem = null;
    private Animator animator;
    private float timer = 0f;
    public float delay;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > delay )
        {
            timer = 0f;
            PlayTrapAnimation();
        }
    }
    
    private void PlayTrapAnimation()
    {
        animationState = !animationState;

        animator.SetBool("Trap", animationState);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (animationState)
        {
            if (collision.CompareTag("Player"))
            {
                healthSystem = collision.GetComponent<HealthSystem>();
                healthSystem.ChangeHealth(-damage);
                Vector3 position = collision.transform.position;
                collision.transform.position = new Vector3(position.x, position.y - 1f, 0);
            }
        }
    }
}
