using UnityEngine;

public class SpawnerBot : MonoBehaviour
{
    private const float ZOffset = 6;
    private const float XOffset = 4;

    [SerializeField] private Bot _bot;
    [SerializeField] private Transform _collectPoint;
    [SerializeField] private CounterOre _counterOre;

    public void Spawn()
    {
        Bot bot = Instantiate(_bot, transform.position + GetRandomPosition(), Quaternion.identity, transform);
        bot.Initialize(_counterOre, _collectPoint);
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(-XOffset, XOffset);
        float positionZ = Random.Range(-ZOffset, ZOffset);

        return new Vector3(positionX, _bot.transform.position.y, positionZ);
    }
}
