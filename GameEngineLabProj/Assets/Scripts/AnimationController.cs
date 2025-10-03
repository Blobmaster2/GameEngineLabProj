using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private Entity _entity;

    private void Start()
    {
        _entity = GetComponent<Entity>();
    }

    private void Update()
    {
        var setWalking = _entity.IsMoving && _entity.IsGrounded;

        _animator.SetBool("isWalking", setWalking);
    }
}
