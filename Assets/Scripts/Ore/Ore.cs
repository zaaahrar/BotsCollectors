using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private int _quantityOre;
    [SerializeField] private bool _isFree = true;

    public bool IsFree => _isFree;
    public int QuantityOre => _quantityOre;

    public void BorrowOre() => _isFree = false;
}
