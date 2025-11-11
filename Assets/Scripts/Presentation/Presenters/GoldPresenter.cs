using System;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class GoldPresenter : IInitializable, IDisposable, IMessageHandler<CurrencyDTO>, IGoldPresenter
    {
        [Inject] private readonly ISubscriber<CurrencyDTO> _currencySubscriber;
        
        [Inject] private readonly IGoldView _goldView;
        
        private DisposableBagBuilder _disposable;

        public void Handle(CurrencyDTO dto) => ChangeGoldValue(dto.Gold);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _currencySubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void ChangeGoldValue(int count)
        {
            _goldView.SetGoldValue(count);
        }

        public void Dispose() => _disposable?.Build().Dispose();

    }
}