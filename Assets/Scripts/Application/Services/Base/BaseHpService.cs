using System;
using Application.Services.Currency;
using Domain.Base;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.Services.Base
{
    public class BaseHpService : IBaseHpService, IInitializable, IDisposable, IMessageHandler<RequestBaseMaxHpDTO>, IMessageHandler<RecoverHpDTO>, IMessageHandler<DroneHitBaseDTO>
    {
        [Inject] private readonly ISubscriber<RequestBaseMaxHpDTO> _requestBaseHpSubscriber;
        [Inject] private readonly ISubscriber<RecoverHpDTO> _recoverHpSubscriber;
        [Inject] private readonly ISubscriber<DroneHitBaseDTO> _droneHitBaseSubscriber;
        
        [Inject] private readonly IPublisher<BaseMaxHpDTO> _maxHpPublisher;
        [Inject] private readonly IPublisher<AddBaseHpResultDTO> _addBaseHpResultPublisher;
        [Inject] private readonly IPublisher<RemoveBaseHpDTO> _removeBaseHpPublisher;

        [Inject] private readonly IBaseConfig _baseConfig;
        [Inject] private readonly IExeptionsConfig _exeptionsConfig;
        
        [Inject] private readonly IGoldService _goldService;

        private IBaseHpModel _baseHpModel;
        private DisposableBagBuilder _disposable;

        public void Handle(RequestBaseMaxHpDTO dto) => GetBaseMaxHp();
        public void Handle(RecoverHpDTO dto) => TryAddHp();
        public void Handle(DroneHitBaseDTO dto) => TakeDamage(dto.DroneModel.Damage);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _requestBaseHpSubscriber.Subscribe(this).AddTo(_disposable);
            _recoverHpSubscriber.Subscribe(this).AddTo(_disposable);
            _droneHitBaseSubscriber.Subscribe(this).AddTo(_disposable);

            _baseHpModel = new BaseHpModel(_baseConfig.MaxHp, _baseConfig.RecoverCost);
        }

        public void GetBaseMaxHp()
        {
            _maxHpPublisher.Publish(new BaseMaxHpDTO(_baseHpModel.MaxHp));
        }

        public void TryAddHp()
        {
            if (!IsAddValid())
                return;

            _goldService.SpendValue(_baseHpModel.RecoverCost);
            _baseHpModel.AddCurrentHp();
            
            _addBaseHpResultPublisher.Publish(new AddBaseHpResultDTO(true));
        }

        public void TakeDamage(int damage)
        {
            _baseHpModel.RemoveCurrentHp(damage);
            _removeBaseHpPublisher.Publish(new RemoveBaseHpDTO(damage));
        }

        private bool IsAddValid()
        {
            if (!_goldService.IsCanSpendGold(_baseHpModel.RecoverCost))
            {
                _addBaseHpResultPublisher.Publish(new AddBaseHpResultDTO(false, _exeptionsConfig.NoMoneyExeption));
                return false;
            }

            if (!_baseHpModel.IsCanAddCurrentHp())
            {
                _addBaseHpResultPublisher.Publish(new AddBaseHpResultDTO(false, _exeptionsConfig.MaxHpExeption));
                return false;
            }
            
            return true;
        }

        public void Dispose() => _disposable?.Build().Dispose();
    }
}