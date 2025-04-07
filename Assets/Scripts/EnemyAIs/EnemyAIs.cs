using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIs : MonoBehaviour
{
  private Vector2 startingPosition;
    private void Start()
    {
        startingPosition = transform.position;
    }

   private Vector2 GetRoamingPosition()
{
    float roamDistance = 5f;
    Vector2 randomDir = new Vector2(
        UnityEngine.Random.Range(-1f, 1f),
        UnityEngine.Random.Range(-1f, 1f)
    ).normalized;

    return startingPosition + randomDir * roamDistance;
}

}
