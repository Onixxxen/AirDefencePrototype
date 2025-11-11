using System;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class BaseHpPresenter : IBaseHpPresenter, IInitializable, IDisposable, IMessageHandler<BaseMaxHpDTO>, IMessageHandler<AddBaseHpResultDTO>, IMessageHandler<RemoveBaseHpDTO>
    {
        [Inject] private readonly ISubscriber<BaseMaxHpDTO> _maxHpSubscriber;
        [Inject] private readonly ISubscriber<AddBaseHpResultDTO> _addBaseHpResultSubscriber;
        [Inject] private readonly ISubscriber<RemoveBaseHpDTO> _removeBaseHpSubscriber;
        
        [Inject] private readonly IBaseHpView _baseHpView;

        private DisposableBagBuilder _disposable;
        
        public void Handle(BaseMaxHpDTO dto) => InitHp(dto.MaxHp);
        public void Handle(AddBaseHpResultDTO dto) => TryDisplayAddHp(dto.Success, dto.Message);
        public void Handle(RemoveBaseHpDTO dto) => DisplayRemoveHp(dto.Count);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _maxHpSubscriber.Subscribe(this).AddTo(_disposable);
            _addBaseHpResultSubscriber.Subscribe(this).AddTo(_disposable);
            _removeBaseHpSubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void InitHp(int maxHp)
        {
            _baseHpView.InitHpBar(maxHp);
        }

        private void TryDisplayAddHp(bool success, string msg)
        {
            if (success)
                _baseHpView.AddHp();
            else 
                _baseHpView.ErrorAddHp(msg);
        }
        
        private void DisplayRemoveHp(int count)
        {
            _baseHpView.RemoveHp(count);
        }

        public void Dispose() => _disposable?.Build().Dispose();
    }
}