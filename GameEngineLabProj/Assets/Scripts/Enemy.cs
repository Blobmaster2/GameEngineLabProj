using UnityEngine;

public class Enemy : Entity, ISpawnable
{
    private float _walkDuration;
    private bool _goingRight;
    [SerializeField] private float _maxWalkDuration;

    protected override void Start()
    {
        base.Start();
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    public GameObject Spawn(Vector2 position)
    {
        return Instantiate(gameObject, position, Quaternion.identity);
    }

    public void Initialize(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (_walkDuration <= _maxWalkDuration)
        {
            if (_goingRight)
            {
                Move(Vector2.right);
            }
            else
            {
                Move(-Vector2.right);
            }
        }
        else
        {
            _walkDuration = 0;
            _goingRight = !_goingRight;
        }

        _walkDuration += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}