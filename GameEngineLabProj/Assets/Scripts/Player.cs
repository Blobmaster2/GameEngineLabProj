using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private GameObject _sprite;

    private bool _facingRight;
    private bool _isGrounded;

    private PlayerInput _playerInput;
    private Rigidbody2D _rb;

    private Vector2 _currentInput => 
        _playerInput.actions["Move"].ReadValue<Vector2>();

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.actions["Attack"].performed += Attack;
        _playerInput.actions["Jump"].performed += Jump;

        _facingRight = true;
        _isGrounded = true;
    }

    private void Update()
    {
        Move();
        UpdateSprite();
    }

    void Attack(InputAction.CallbackContext ctx)
    {

    }

    private void Move()
    {
        _rb.AddForce(new Vector2(_currentInput.x, 0));

        _rb.linearVelocity = new Vector2(Mathf.Clamp(_rb.linearVelocityX, -_maxSpeed, _maxSpeed), _rb.linearVelocityY);

        if (_currentInput.x == 0)
        {
            return;
        }

        _facingRight = _currentInput.x > 0 ? true : false;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log(_isGrounded);

        if (!_isGrounded)
        {
            return;
        }

        _rb.AddForce(Vector2.up * _jumpForce);
        _isGrounded = false;
        
    }

    private void UpdateSprite()
    {
        _sprite.transform.localScale = _facingRight ? Vector3.one : new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }
}
