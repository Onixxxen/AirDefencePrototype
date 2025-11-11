public interface IBaseHpModel
{
    int MaxHp { get; }
    int CurrentHp { get; }
    int RecoverCost { get; }
    void AddCurrentHp();
    void RemoveCurrentHp(int count);
    bool IsDead();
    bool IsCanAddCurrentHp();
}