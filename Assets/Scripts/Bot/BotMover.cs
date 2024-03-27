using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Bot))]
public class BotMover : MonoBehaviour
{
    private const float MaxDistanceToObject = 1f;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _collectionPosition;
    [SerializeField] private CounterOre _counterOre;
    [SerializeField] private Base _base;

    private Vector3 _startPosition;
    private Bot _bot;

    public Base Base => _base;
    public Vector3 StartPosition => _startPosition;
    public Transform CollectionPosition => _collectionPosition;
    public Bot Bot => _bot;
    public CounterOre CounterOre => _counterOre;

    private void Awake()
    {
        _startPosition = transform.position;
        _bot = GetComponent<Bot>(); 
    }

    public void Initialize(CounterOre counterOre, Transform collectPoint)
    {
        _counterOre = counterOre;
        ChangeCollectionPosition(collectPoint);
    }

    protected IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > MaxDistanceToObject)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }
    }

    protected void ChangeStartPosition(Vector3 newPosition) => _startPosition = newPosition;

    public void ChangeCollectionPosition(Transform newPosition) => _collectionPosition = newPosition;
}
