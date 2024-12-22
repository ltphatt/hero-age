using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 10;

    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    public LayerMask attackMask;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position + attackOffset, transform.forward, attackRange);
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().ChangeHealth(-attackDamage);
        }
    }
}
