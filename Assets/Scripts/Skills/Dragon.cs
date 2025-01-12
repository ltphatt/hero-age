using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : SpecialSkill
{
    [SerializeField] private int burnDamage = 1;
    [SerializeField] private float burnDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        enemy?.Burn(burnDamage, burnDuration);
    }
}
