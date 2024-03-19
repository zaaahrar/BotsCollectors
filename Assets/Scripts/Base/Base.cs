using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Transform _parentOre;
    [SerializeField] private Transform _parentBot;

    private Bot[] _botsArray;
    private Ore[] _oresArray;

    private void Awake() => _botsArray = _parentBot.GetComponentsInChildren<Bot>();

    public void TrySendingBot()
    {
        ScanArea();

        foreach(Bot bot in _botsArray)
        {
            if (bot.IsAvailable)
            {
                foreach(Ore ore in _oresArray)
                {
                    if (ore.IsFree)
                    {
                        bot.GetTarget(ore);
                        break;
                    }
                }
            }          
        }
    }

    private void ScanArea() => _oresArray = _parentOre.GetComponentsInChildren<Ore>();
}
