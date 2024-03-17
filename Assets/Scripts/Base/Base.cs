using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private List<Ore> _ores;
    [SerializeField] private Transform _parentOre;
    [SerializeField] private Transform _parentBot;

    private Bot[] _botsArray;
    private Ore[] _oresArray;

    private void Awake()
    {
        _botsArray = _parentBot.GetComponentsInChildren<Bot>();
        _bots = new List<Bot>(_botsArray);
    }

    public void TrySendingBot()
    {
        ScanArea();

        foreach(Bot bot in _bots)
        {
            if (bot.IsAvailable)
            {
                foreach(Ore ore in _ores)
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

    private void ScanArea()
    {
        _oresArray = _parentOre.GetComponentsInChildren<Ore>();
        _ores = new List<Ore>(_oresArray);
    }
}
