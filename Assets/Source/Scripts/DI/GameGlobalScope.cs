using Source.Scripts.Services;
using Source.Scripts.Services.AudioService;
using Source.Scripts.Services.SavingService;
using Source.Scripts.Services.SceneService;
using Source.Scripts.Services.Settings;
using Source.Scripts.UI.LoadingScreen;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.DI
{
   public class GameGlobalScope : LifetimeScope
   {
      [SerializeField] private LoadingScreenView _loadingScreenView;
      [SerializeField] private SceneConfig _sceneConfig;
      [SerializeField] private DefaultGameSettingsConfig _defaultSettingsConfig;
      [SerializeField] private Transform _audioContainer;
      
      protected override void Configure(IContainerBuilder builder)
      {
         builder.RegisterEntryPoint<SavingService>().As<ISavingService>();
         builder.RegisterEntryPoint<AudioService>().As<IAudioService>().WithParameter(_audioContainer);
         builder.RegisterEntryPoint<SettingsService>().As<ISettingsProvider>().WithParameter(_defaultSettingsConfig);
       
         builder.Register<ISceneService,SceneService>(Lifetime.Singleton).WithParameter(_sceneConfig);
         builder.Register<LoadingScreenProvider>(Lifetime.Singleton).WithParameter(_loadingScreenView);
       
         builder.RegisterEntryPoint<GameBootstrap>();
         
         Debug.Log($"[{nameof(GameGlobalScope)}] Initialized.");
      }
   }
}
