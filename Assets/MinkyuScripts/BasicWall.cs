using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWall : MonoBehaviour
{
    private float maxDuravity = 40f;
    public float currentDuravity = 40f;

    private float recoveryTime;
    private bool isDamaged;

    public void DamagedWall(float dmg)
    {
        isDamaged = true;
        currentDuravity -= dmg;
        if (currentDuravity <= maxDuravity/2)
        {
            // 이미지 바꿔주기
        }
        else if(currentDuravity <= 0)
        {
            Destroy(gameObject);
            // 만약 오브젝트 Pool로 벽을 구현하면 SetActive(false)로.
        }
        RecoveryDmg();
    }

    private void RecoveryDmg()
    {
        if (currentDuravity < maxDuravity)
        {
            recoveryTime = 0f;
            recoveryTime += Time.deltaTime;// 코루틴으로
            if (isDamaged && recoveryTime >= 5f)
            {
                currentDuravity = maxDuravity;
                isDamaged = false;
                recoveryTime = 0;
                // 이미지 처음 껄로 바꿔주기.
            }
        }
    }
}
