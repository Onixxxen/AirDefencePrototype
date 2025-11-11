using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyConfig", menuName = "ScriptableObjects/Configs/CurrencyConfig")]
public class CurrencyConfig : ScriptableObject, ICurrencyConfig
{
    [SerializeField] private int _goldStartCount = 100;

    public int GoldStartCount => _goldStartCount;
}