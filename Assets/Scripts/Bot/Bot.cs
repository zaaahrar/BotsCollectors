using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _basePosition;
    [SerializeField] private CounterOre _counterOre;
    [SerializeField] private bool _isAvailable;
    [SerializeField] private float _speed;

    private Vector3 _startPosition;
    private float _turnForward = 0;
    private float _turnBack = 160;
    private float _maxDistanceToObject = 1.5f;
    private Ore _ore = null;

    public bool IsAvailable => _isAvailable;

    private void Awake() => _startPosition = transform.position;

    public void GetTarget(Ore ore)
    {
        _isAvailable = false;
        ore.BorrowOre();
        _ore = ore;
        StartCoroutine(BringOre());
    }

    private IEnumerator BringOre()
    {
        Vector3 finishPosition = new Vector3(_ore.transform.position.x, transform.position.y, _ore.transform.position.z);

        yield return MoveTo(finishPosition);

        _ore.transform.parent = _hand;
        _ore.transform.position = _hand.position;
        TurnAround(_turnBack);

        finishPosition = new Vector3(_basePosition.position.x, transform.position.y, _basePosition.position.z);

        yield return MoveTo(finishPosition);

        Destroy(_ore.gameObject);
        GiveOre();
        TurnAround(_turnForward);

        yield return MoveTo(_startPosition);

        _isAvailable = true;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > _maxDistanceToObject)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void GiveOre() => _counterOre.AddOre(_ore.QuantityOre);

    private void TurnAround(float turn) => transform.rotation = Quaternion.Euler(0, turn, 0);
}
