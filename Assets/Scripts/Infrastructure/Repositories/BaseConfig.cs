using UnityEngine;

[CreateAssetMenu(fileName = "BaseConfig", menuName = "ScriptableObjects/Configs/BaseConfig")]
public class BaseConfig : ScriptableObject, IBaseConfig
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _recoverCost;
    
    public int MaxHp => _maxHp;
    public int RecoverCost => _recoverCost;
}