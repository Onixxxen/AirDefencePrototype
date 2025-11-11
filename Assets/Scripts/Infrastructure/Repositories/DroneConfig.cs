using UnityEngine;

namespace Infrastructure.Repositories
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "ScriptableObjects/Configs/DroneConfig")]
    public class DroneConfig : ScriptableObject, IDroneConfig
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private int _award;
        
        [Header("Fly Settings")]
        [SerializeField] private int _maxHeight;
        [SerializeField] private int _minHeight;
        [SerializeField] private float _maxTurnAngle;
        [SerializeField] private float _maxBankAngle;
        [SerializeField] private float _bankSpeed;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _heightLerpSpeed;
        
        public float Speed => _speed;
        public int Damage => _damage;
        public int Award => _award;
        public int MaxHeight => _maxHeight;
        public int MinHeight => _minHeight;
        public float MaxTurnAngle => _maxTurnAngle;
        public float MaxBankAngle => _maxBankAngle;
        public float BankSpeed => _bankSpeed;
        public float TurnSpeed => _turnSpeed;
        public float HeightLerpSpeed => _heightLerpSpeed;
    }
}