using UnityEngine;

public class Samwich : MonoBehaviour
{
    [SerializeField] private float _rotationRate;

    // Update is called once per frame
    void Update()
    {
        var newRot = transform.rotation.eulerAngles.y + Time.deltaTime * _rotationRate;

        transform.rotation = Quaternion.Euler(0, newRot, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.YouWin();
        }
    }
}
