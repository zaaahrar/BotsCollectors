using UnityEngine;
using TMPro;

public class DisplayOre : MonoBehaviour
{
    [SerializeField] private TMP_Text _OreText;

    public void UpdateOre(int value) => _OreText.text = $"Ore: {value}";
}
