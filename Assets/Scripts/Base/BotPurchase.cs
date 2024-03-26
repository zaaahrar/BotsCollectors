using UnityEngine;

public class BotPurchase : MonoBehaviour
{
    [SerializeField] private CounterOre _counterOre;
    [SerializeField] private SpawnerBot _spawnerBot;
    [SerializeField] private int _botPrice;

    public void BuyBot()
    {
        if (_counterOre.CountOre >= _botPrice)
        {
            _spawnerBot.Spawn();
            _counterOre.SpendOre(_botPrice);
        }
    }
}
