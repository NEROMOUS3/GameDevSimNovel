using Source.Scripts.Services.SceneService;
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
      
      protected override void Configure(IContainerBuilder builder)
      {
         builder.Register<ISceneService,SceneService>(Lifetime.Singleton).WithParameter(_sceneConfig);
         builder.Register<LoadingScreenProvider>(Lifetime.Singleton).WithParameter(_loadingScreenView);
         
         Debug.Log($"[{nameof(GameGlobalScope)}] Initialized.");
      }
   }
}
