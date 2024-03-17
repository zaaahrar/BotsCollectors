using UnityEngine;

public class CounterOre : MonoBehaviour
{
    [SerializeField] private int _countOre;
    [SerializeField] private DisplayOre _displayOre;

    private void Start() => _displayOre.UpdateOre(_countOre);

    public void AddOre(int value)
    {
        _countOre += value;
        _displayOre.UpdateOre(_countOre);
    }
}
