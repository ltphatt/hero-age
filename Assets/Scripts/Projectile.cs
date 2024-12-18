using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    float timer = 0f;
    [SerializeField] float speed = 5f;
    [SerializeField] float timeLife = 5f;
    PlayerMovement playerMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
        playerMovement = FindObjectOfType<PlayerMovement>();
        speed = playerMovement.transform.forward.z * speed;
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
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
