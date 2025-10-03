using UnityEngine;

public class Enemy : Entity
{
    private float _walkDuration;
    private bool _goingRight;
    [SerializeField] private float _maxWalkDuration;

    void Update()
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
            GameManager.Instance.RemoveLives();
        }
    }
}