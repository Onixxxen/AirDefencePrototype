namespace Domain.Base
{
    public class BaseHpModel : IBaseHpModel
    {
        private int _maxHp;
        private int _currentHp;
        private int _recoverCost;

        public int MaxHp => _maxHp;
        public int CurrentHp => _currentHp;
        public int RecoverCost => _recoverCost;

        public BaseHpModel(int maxHp, int recoverCost)
        {
            _maxHp = maxHp;
            _currentHp = _maxHp;
            _recoverCost = recoverCost;
        }

        public void AddCurrentHp()
        {
            if (!IsCanAddCurrentHp())
                return;
            
            _currentHp++;
        }

        public void RemoveCurrentHp(int count)
        {
            if (!IsCanRemoveCurrentHp(count))
                return;

            _currentHp -= count;
        }

        public bool IsDead() => _currentHp <= 0;
        public bool IsCanAddCurrentHp() => _currentHp < _maxHp;
        private bool IsCanRemoveCurrentHp(int count) => count > 0 && !IsDead();
    }
}