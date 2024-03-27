using UnityEngine;

public class SpawnerBot : MonoBehaviour
{
    [SerializeField] private BotMover _bot;
    [SerializeField] private Transform _collectPoint;
    [SerializeField] private CounterOre _counterOre;
    [SerializeField] private float ZOffset = 6;
    [SerializeField] private float XOffset = 4;

    public void Spawn()
    {
        BotMover bot = Instantiate(_bot, transform.position + GetRandomPosition(), Quaternion.identity, transform);
        bot.Initialize(_counterOre, _collectPoint);
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(-XOffset, XOffset);
        float positionZ = Random.Range(-ZOffset, ZOffset);

        return new Vector3(positionX, _bot.transform.position.y, positionZ);
    }
}
