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
    public Action BuildBaseAction;

    [SerializeField] private bool _isBaseStationPriority = false;
    private Transform _newBasePosition = null;
    private Coroutine _currentCoroutine = null;
    private Bot[] _botsArray;
    private Ore[] _oresArray;

    public Transform CollectPoint => _collectPoint;
    public bool IsBaseStationPriority => _isBaseStationPriority;
    public int ConstructionCost => _constructionCost;

    private void Awake() => ScanBots();

    private void OnEnable() => BuildBaseAction += BuildNewBase;

    private void OnDisable() => BuildBaseAction -= BuildNewBase;

    public void BuildNewBase()
    {
        foreach (Bot bot in _botsArray)
        {
            if (bot.IsAvailable)
            {
                bot.BuildBase.GetPositionNewBase(_newBasePosition);
                _counterOre.SpendOre(_constructionCost);

                if (_currentCoroutine != null)
                    StopCoroutine(_currentCoroutine);

                _currentCoroutine = StartCoroutine(bot.BuildBase.BeginBuildBase(this));
                break;
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
                foreach (Ore ore in _oresArray)
                {
                    if (ore.IsFree)
                    {
                        bot.CollectsResource.GetOre(ore);
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
