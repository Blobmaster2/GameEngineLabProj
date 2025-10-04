using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private float _jumpForce;

    public PlayerInput PlayerInput {  get; private set; }

    private Vector2 _currentInput => 
        PlayerInput.actions["Move"].ReadValue<Vector2>();

    protected override void Start()
    {
        base.Start();

        PlayerInput = GetComponent<PlayerInput>();

        PlayerInput.actions["Attack"].performed += Attack;
        PlayerInput.actions["Jump"].performed += Jump;

        _facingRight = true;
    }

    private void OnDisable()
    {
        PlayerInput.actions["Attack"].performed -= Attack;
        PlayerInput.actions["Jump"].performed -= Jump;
    }

    private void FixedUpdate()
    {
        Move(_currentInput);
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        //todo
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!IsGrounded)
        {
            return;
        }

        _rb.AddForce(Vector2.up * _jumpForce);
    }

    public override void Die()
    {
        GameManager.Instance.RemoveLives();
    }
}
