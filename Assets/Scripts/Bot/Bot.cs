using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{
    private const float MaxDistanceToObject = 1f;
    private const float TurnForward = 0;
    private const float TurnBack = 160;

    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _collectionPosition;
    [SerializeField] private CounterOre _counterOre;
    [SerializeField] private bool _isAvailable;
    [SerializeField] private float _speed;
    [SerializeField] private Base _basePrefab;

    private Transform _newBasePostion = null;
    private Vector3 _startPosition;
    private Ore _ore = null;

    public bool IsAvailable => _isAvailable;

    private void Awake() => _startPosition = transform.position;

    public void GetOre(Ore ore)
    {
        _isAvailable = false;
        ore.BorrowOre();
        _ore = ore;
        StartCoroutine(BringOre());
    }

    public void Initialize(CounterOre counterOre, Transform collectionPosition)
    {
        _counterOre = counterOre;
        _collectionPosition = collectionPosition;
    }

    public void GetPositionNewBase(Transform newBasePosition) => _newBasePostion = newBasePosition;

    public IEnumerator BuildBase(Base currentBase)
    {
        _isAvailable = false;
        Vector3 finishPosition = new Vector3(_newBasePostion.position.x, transform.position.y, _newBasePostion.position.z);

        yield return MoveTo(finishPosition);

        _startPosition = transform.position;
        Base newBase = Instantiate(_basePrefab, _newBasePostion.position, Quaternion.identity);
        currentBase.RemoveFlagAction?.Invoke();
        currentBase.RemoveBaseConstructionPriority();
        _collectionPosition = newBase.CollectPoint;
        _isAvailable = true;
    }

    private IEnumerator BringOre()
    {
        Vector3 finishPosition = new Vector3(_ore.transform.position.x, transform.position.y, _ore.transform.position.z);

        yield return MoveTo(finishPosition);

        _ore.transform.parent = _hand;
        _ore.transform.position = _hand.position;
        TurnAround(TurnBack);

        finishPosition = new Vector3(_collectionPosition.position.x, transform.position.y, _collectionPosition.position.z);

        yield return MoveTo(finishPosition);

        Destroy(_ore.gameObject);
        GiveOre();
        TurnAround(TurnForward);

        yield return MoveTo(_startPosition);

        _isAvailable = true;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > MaxDistanceToObject)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void GiveOre() => _counterOre.AddOre(_ore.QuantityOre);

    private void TurnAround(float turn) => transform.rotation = Quaternion.Euler(0, turn, 0);
}
