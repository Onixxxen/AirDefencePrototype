public interface ICurrencyModel
{
    int Count { get; }
    void Add(int count);
    void Remove(int count);
}