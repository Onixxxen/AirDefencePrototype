using UnityEngine;

namespace Infrastructure.Repositories
{
    [CreateAssetMenu(fileName = "ExeptionsConfig", menuName = "ScriptableObjects/Configs/ExeptionsConfig")]
    public class ExeptionsConfig : ScriptableObject, IExeptionsConfig
    {
        [SerializeField] private string _noMoneyExeption;
        [SerializeField] private string _maxHpExeption;
        
        public string NoMoneyExeption => _noMoneyExeption;
        public string MaxHpExeption => _maxHpExeption;
    }
}