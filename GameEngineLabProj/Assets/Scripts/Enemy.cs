using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float _walkDuration;
    [SerializeField] private float _maxWalkDuration;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _maxWalkSpeed;
    private bool _goingRight;

    [SerializeField] private GameObject _sprite;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_walkDuration <= _maxWalkDuration)
        {
            if (_goingRight)
            {
                _rb.AddForce(Vector2.right * _walkSpeed);
            }
            else
            {
                _rb.AddForce(-Vector2.right * _walkSpeed);
            }
        }
        else
        {
            _goingRight = !_goingRight;
            _walkDuration = 0;
        }

        _rb.linearVelocity = new Vector2(Mathf.Clamp(_rb.linearVelocityX, -_maxWalkSpeed, _maxWalkSpeed), _rb.linearVelocityY);

        _walkDuration += Time.deltaTime;

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        _sprite.transform.localScale = !_goingRight ? Vector3.one : new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.GameOver();
        }
    }
}