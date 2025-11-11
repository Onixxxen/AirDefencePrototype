namespace Domain.Currency
{
    public class GoldModel : ICurrencyModel
    {
        private int _count;

        public int Count => _count;

        public GoldModel(int count)
        {
            _count = count;
        }

        public void Add(int count)
        {
            if (count <= 0)
                return;
            
            _count += count;
        }

        public void Remove(int count)
        {
            if (_count < count || count <= 0)
                return;

            _count -= count;
        }
    }
}