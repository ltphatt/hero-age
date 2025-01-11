using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : SpecialSkill
{
    [SerializeField] float stunDuration = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Stunned(stunDuration);
        }
    }
}
