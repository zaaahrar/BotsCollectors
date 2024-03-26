using UnityEngine;

public class CounterOre : MonoBehaviour
{
    [SerializeField] private int _countOre;
    [SerializeField] private OreTextDisplay _oreTextDisplay;

    public int CountOre => _countOre;

    private void Start() => _oreTextDisplay.UpdateOre(_countOre);

    public void AddOre(int value)
    {
        _countOre += value;
        _oreTextDisplay.UpdateOre(_countOre);
    }

    public void SpendOre(int value)
    {
        _countOre -= value;
        _oreTextDisplay.UpdateOre(_countOre);
    }
}
