using System;
using Domain.Currency;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.Services.Currency
{
    public class GoldService : IGoldService, IInitializable, IDisposable, IMessageHandler<RequestCurrencyDTO>
    {
        [Inject] private readonly ISubscriber<RequestCurrencyDTO> _requestCurrencySubscriber;
        [Inject] private readonly IPublisher<CurrencyDTO> _currencyPublisher;
        [Inject] private readonly ICurrencyConfig _currencyConfig;

        private ICurrencyModel _gold;
        private DisposableBagBuilder _disposable;
        
        public void Handle(RequestCurrencyDTO message) => GetValue();

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _requestCurrencySubscriber.Subscribe(this).AddTo(_disposable);

            _gold = new GoldModel(_currencyConfig.GoldStartCount);
        }

        public void AddValue(int count)
        {
            _gold.Add(count);
            _currencyPublisher.Publish(new CurrencyDTO(_gold.Count));
        }

        public void SpendValue(int count)
        {
            _gold.Remove(count);
            _currencyPublisher.Publish(new CurrencyDTO(_gold.Count));
        }

        public void GetValue()
        {
            _currencyPublisher.Publish(new CurrencyDTO(_gold.Count));
        }

        public bool IsCanSpendGold(int count) => count <= _gold.Count;
        
        public void Dispose() => _disposable?.Build().Dispose();
    }
}