using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool IsMoving => _isMoving;
    private bool _isMoving;

    public bool IsGrounded => _isGrounded;
    private bool _isGrounded = true;

    protected bool _facingRight;

    [SerializeField] private bool _flipImage;
    [SerializeField] private GameObject _sprite;

    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _moveSpeed;

    protected Rigidbody2D _rb;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void UpdateSprite()
    {
        var imageOrientation = _flipImage ? !_facingRight : _facingRight;

        _sprite.transform.localScale = imageOrientation ? Vector3.one : new Vector3(-1, 1, 1);
    }

    protected virtual void Move(Vector2 dir)
    {
        _rb.AddForce(new Vector2(dir.x, 0));

        _rb.linearVelocity = new Vector2(Mathf.Clamp(_rb.linearVelocityX, -_maxSpeed, _maxSpeed), _rb.linearVelocityY);

        if (dir.x > 0)
        {
            _isMoving = true;
            _facingRight = true;
        }
        else if (dir.x < 0)
        {
            _isMoving = true;
            _facingRight = false;
        }
        else
        {
            _isMoving = false;
        }

        UpdateSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}
