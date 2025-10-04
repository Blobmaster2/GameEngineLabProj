using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _spawnLocation;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2 _spawnForce;
    private float _spawnTime;

    private void Update()
    {
        if (_spawnTime >= _spawnDelay)
        {
            var obj = ObjectFactory.CreateObject(_objectToSpawn.GetComponent<ISpawnable>(), _spawnLocation.position);
            obj.GetComponent<ISpawnable>().Initialize(_spawnForce);
            _spawnTime = 0;
        }

        _spawnTime += Time.deltaTime;
    }
}
