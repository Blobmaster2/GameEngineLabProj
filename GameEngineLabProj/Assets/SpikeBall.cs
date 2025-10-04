using UnityEngine;

public class SpikeBall : MonoBehaviour, ISpawnable
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < -30)
        {
            Despawn();
        }
    }

    public GameObject Spawn(Vector2 position)
    {
        return Instantiate(gameObject, position, Quaternion.identity);
    }

    public void Initialize(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<Player>().Die();
        }
    }
}
