using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttacks
{
    public static IEnumerator JumpAndDamage(Transform attacker, Transform target, float attackDamage, float jumpHeight, System.Action onLanded)
    {
        Vector3 startPosition = attacker.position;
        Vector3 targetPosition = target.position;
        float jumpDuration = 1.0f; // 점프선딜 해보고 조정
        float elapsedTime = 0;

        while (elapsedTime < jumpDuration)
        {
            float height = Mathf.Sin(Mathf.PI * elapsedTime / jumpDuration) * jumpHeight;
            attacker.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / jumpDuration) + Vector3.up * height;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attacker.position = targetPosition;
        onLanded?.Invoke();
    }
}
