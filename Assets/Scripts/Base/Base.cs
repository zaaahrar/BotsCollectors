using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Transform _parentOre;
    [SerializeField] private Transform _parentBot;
    [SerializeField] private Transform _collectPoint;
    [SerializeField] private CounterOre _counterOre;
    [SerializeField] private int _constructionCost = 5;

    public Action RemoveFlagAction;

    private bool _isBaseStationPriority = false;
    private Transform _newBasePosition = null;
    private Bot[] _botsArray;
    private Ore[] _oresArray;

    public Transform CollectPoint => _collectPoint;

    private void Awake() => ScanBots();

    private void Update()
    {
        if (_isBaseStationPriority && _counterOre.CountOre >= _constructionCost)
        {
            foreach(Bot bot in _botsArray)
            {
                if (bot.IsAvailable)
                {
                    bot.GetPositionNewBase(_newBasePosition);
                    StartCoroutine(bot.BuildBase(this));
                    _counterOre.SpendOre(_constructionCost);
                    break;
                }
            }
        }
    }   


    public void TrySendingBot()
    {
        ScanArea();
        ScanBots();

        foreach (Bot bot in _botsArray)
        {
            if (bot.IsAvailable)
            {
                foreach(Ore ore in _oresArray)
                {
                    if (ore.IsFree)
                    {
                        bot.GetOre(ore);
                        break;
                    }
                }
            }          
        }
    }

    public void GetPostionNewBase(Transform position) => _newBasePosition = position;

    public void SetBaseConstructionPriority() => _isBaseStationPriority = true;

    public void RemoveBaseConstructionPriority() => _isBaseStationPriority = false;

    private void ScanArea() => _oresArray = _parentOre.GetComponentsInChildren<Ore>();

    private void ScanBots() => _botsArray = _parentBot.GetComponentsInChildren<Bot>();
}
