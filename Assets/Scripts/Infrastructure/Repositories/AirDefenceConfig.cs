using UnityEngine;

[CreateAssetMenu(fileName = "AirDefenceConfig", menuName = "ScriptableObjects/Configs/AirDefenceConfig")]
public class AirDefenceConfig : ScriptableObject, IAirDefenceConfig
{
    [SerializeField] private float _missileSpeed;
    [SerializeField] private float _reloadSpeed;

    public float MissileSpeed => _missileSpeed;
    public float ReloadSpeed => _reloadSpeed;
}