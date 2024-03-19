using UnityEngine;
using System.Collections;

public class SpawnerOre : MonoBehaviour
{

    private const float Offset = 7f;

    [SerializeField] private Ore _ore;
    [SerializeField] private float _spawnDelayTime;
    [SerializeField] private bool _canSpawned;

    private WaitForSeconds _spawnDelay;

    private void Awake() => _spawnDelay = new WaitForSeconds(_spawnDelayTime);

    private void Update()
    {
        if (_canSpawned)
        {
            _canSpawned = false;
            Spawn();
            StartCoroutine(DelayingSpawn());
        }
    }

    private void Spawn() => Instantiate(_ore, transform.position + GetRandomPosition(), Quaternion.identity, transform);

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(-Offset, Offset);
        float positionZ = Random.Range(-Offset, Offset);

        return new Vector3(positionX, _ore.transform.position.y, positionZ);
    }

    private IEnumerator DelayingSpawn()
    {
        yield return _spawnDelay;
        _canSpawned = true;
    }
}
