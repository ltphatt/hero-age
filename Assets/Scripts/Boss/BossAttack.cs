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
    private float attackOffsetX = 0f;
    [SerializeField] private string bossType;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        attackOffsetX = attackOffset.x;
    }

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
        if (bossType == "Mallet")
        {
            audioManager.PlaySFX(audioManager.malletAttack, gameObject);
        }
        else if (bossType == "Sword")
        {
            audioManager.PlaySFX(audioManager.swordAttack, gameObject);
        }
        else if (bossType == "Axe")
        {
            audioManager.PlaySFX(audioManager.axeAttack, gameObject);
        }
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {

            colInfo.GetComponent<PlayerController>().ChangeHealth(-attackDamage);
        }
    }

    public void ChangeAttackOffset()
    {
        // if isFlipped
        if (GetComponent<Boss>().isFlipped)
        {
            attackOffset.x = -attackOffsetX;
        }
        else
        {
            attackOffset.x = attackOffsetX;
        }
    }
}
