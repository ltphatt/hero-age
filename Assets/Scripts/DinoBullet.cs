using UnityEngine;

public class DinoBullet : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int damage = 1;
    public Transform target;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!target) return;

        RotateFollowPlayer();

        Vector2 direction = (target.position - transform.position + Vector3.up).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-damage);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void RotateFollowPlayer()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public int GetDamage()
    {
        return damage;
    }
}