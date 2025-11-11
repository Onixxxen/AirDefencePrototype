namespace Application.Services.Currency
{
    public interface IGoldService
    {
        void AddValue(int count);
        void SpendValue(int count);
        void GetValue();
        bool IsCanSpendGold(int count);
    }
}