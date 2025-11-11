using Application.Services.AirDefence;
using Application.Services.Base;
using Application.Services.Camera;
using Application.Services.Currency;
using Application.Services.Drone;
using Infrastructure.Factory;
using Infrastructure.Repositories;
using MessagePipe;
using Presentation.Presenters;
using Presentation.Views.AirDefence;
using Presentation.Views.Base;
using Presentation.Views.Currency;
using Presentation.Views.Drone;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class Bootstrapper : LifetimeScope
    {
        [Header("Views")]
        [SerializeField] private CameraMoveView _cameraMoveView;
        [SerializeField] private CameraZoomView _cameraZoomView;
        [SerializeField] private CameraRotateView _cameraRotateView;
        [SerializeField] private BaseHpView _baseHpView;
        [SerializeField] private GoldView _goldView;
        [SerializeField] private AirDefenceView _airDefenceView;
        [SerializeField] private DroneView _droneView;
        
        [Header("Configs")]
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private CurrencyConfig _currencyConfig;
        [SerializeField] private BaseConfig _baseConfig;
        [SerializeField] private ExeptionsConfig _exeptionsConfig;
        [SerializeField] private AirDefenceConfig _airDefenceConfig;
        [SerializeField] private DroneConfig _droneConfig;
        [SerializeField] private DroneSpawnConfig _droneSpawnConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();

            builder.RegisterMessageBroker<CameraMoveDTO>(options);
            builder.RegisterMessageBroker<CameraZoomDTO>(options);
            builder.RegisterMessageBroker<CameraMoveResultDTO>(options);
            builder.RegisterMessageBroker<CameraZoomResultDTO>(options);
            builder.RegisterMessageBroker<CurrencyDTO>(options);
            builder.RegisterMessageBroker<RequestCurrencyDTO>(options);
            builder.RegisterMessageBroker<RecoverHpDTO>(options);
            builder.RegisterMessageBroker<RequestBaseMaxHpDTO>(options);
            builder.RegisterMessageBroker<AddBaseHpResultDTO>(options);
            builder.RegisterMessageBroker<BaseMaxHpDTO>(options);
            builder.RegisterMessageBroker<RemoveBaseHpDTO>(options);
            builder.RegisterMessageBroker<DroneDestroyDTO>(options);
            builder.RegisterMessageBroker<DroneHitBaseDTO>(options);
            builder.RegisterMessageBroker<DronePathDTO>(options);
            builder.RegisterMessageBroker<DroneSpottedBaseDTO>(options);
            builder.RegisterMessageBroker<DroneStartedFlyDTO>(options);
            builder.RegisterMessageBroker<SpawnDroneDTO>(options);
            builder.RegisterMessageBroker<DroneHitByMissileDTO>(options);
            builder.RegisterMessageBroker<DroneEnterADZoneDTO>(options);
            builder.RegisterMessageBroker<DroneHitDTO>(options);
            builder.RegisterMessageBroker<MissilePathDTO>(options);
            
            builder.RegisterInstance(_cameraMoveView as ICameraMoveView);
            builder.RegisterInstance(_cameraZoomView as ICameraZoomView);
            builder.RegisterInstance(_cameraRotateView as ICameraRotateView);
            builder.RegisterInstance(_baseHpView as IBaseHpView);
            builder.RegisterInstance(_goldView as IGoldView);
            builder.RegisterInstance(_airDefenceView as IAirDefenceView);
            builder.RegisterInstance(_droneView);
            
            builder.RegisterInstance(_cameraConfig as ICameraConfig);
            builder.RegisterInstance(_currencyConfig as ICurrencyConfig);
            builder.RegisterInstance(_baseConfig as IBaseConfig);
            builder.RegisterInstance(_exeptionsConfig as IExeptionsConfig);
            builder.RegisterInstance(_airDefenceConfig as IAirDefenceConfig);
            builder.RegisterInstance(_droneConfig as IDroneConfig);
            builder.RegisterInstance(_droneSpawnConfig as IDroneSpawnConfig);
            
            builder.Register<PlayerInputActions>(Lifetime.Singleton);
            builder.Register<DroneFactory>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<CameraPresenter>(Lifetime.Singleton).As<ICameraPresenter>();
            builder.RegisterEntryPoint<BaseHpPresenter>(Lifetime.Singleton).As<IBaseHpPresenter>();
            builder.RegisterEntryPoint<GoldPresenter>(Lifetime.Singleton).As<IGoldPresenter>();
            builder.RegisterEntryPoint<AirDefencePresenter>(Lifetime.Singleton).As<IAirDefencePresenter>();
            builder.RegisterEntryPoint<DronePresenter>(Lifetime.Singleton).As<IDronePresenter>();

            builder.RegisterEntryPoint<CameraMoveService>(Lifetime.Singleton).As<ICameraMoveService>();
            builder.RegisterEntryPoint<CameraZoomService>(Lifetime.Singleton).As<ICameraZoomService>();
            builder.RegisterEntryPoint<GoldService>(Lifetime.Singleton).As<IGoldService>();
            builder.RegisterEntryPoint<BaseHpService>(Lifetime.Singleton).As<IBaseHpService>();
            builder.RegisterEntryPoint<AirDefenceService>(Lifetime.Singleton).As<IAirDefenceService>();
            builder.RegisterEntryPoint<DroneService>(Lifetime.Singleton).As<IDroneService>();
        }
    }
}