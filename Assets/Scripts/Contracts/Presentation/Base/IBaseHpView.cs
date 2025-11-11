public interface IBaseHpView
{
    void InitHpBar(int maxHp);
    void AddHp();
    void RemoveHp(int count);
    void ErrorAddHp(string msg);
}