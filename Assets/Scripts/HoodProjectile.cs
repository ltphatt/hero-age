using UnityEngine;

public class HoodProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    float timer = 0f;
    [SerializeField] float speed = 5f;
    [SerializeField] float timeLife = 5f;
    [SerializeField] int damage = 1;
    PlayerController player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
        player = FindObjectOfType<PlayerController>();
        speed = player.transform.forward.z * speed;
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed, 0f);

        timer += Time.deltaTime;
        if (timer > timeLife)
        {
            Destroy(gameObject);
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.ChangeHealth(-damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
