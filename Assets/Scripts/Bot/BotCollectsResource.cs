using System.Collections;
using UnityEngine;

public class BotCollectsResource : BotMover
{
    private const float TurnForward = 0;
    private const float TurnBack = 160;

    [SerializeField] private Transform _hand;

    private Ore _ore = null;

    public void GetOre(Ore ore)
    {
        Bot.Borrow();
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
        TurnAround(TurnBack);

        finishPosition = new Vector3(CollectionPosition.position.x, transform.position.y, CollectionPosition.position.z);

        yield return MoveTo(finishPosition);

        Destroy(_ore.gameObject);
        GiveOre();
        TurnAround(TurnForward);

        if (Base.IsBaseStationPriority)
        {
            Debug.Log("Check");
            CounterOre.CheckOreToBuildBase(Base);
        }

        yield return MoveTo(StartPosition);

        Bot.Release();
    }

    private void GiveOre() => CounterOre.AddOre(_ore.QuantityOre);

    private void TurnAround(float turn) => transform.rotation = Quaternion.Euler(0, turn, 0);
}
