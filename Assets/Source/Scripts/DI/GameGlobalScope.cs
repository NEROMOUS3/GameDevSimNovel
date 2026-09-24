using Source.Scripts.Services.SceneService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.DI
{
   public class GameGlobalScope : LifetimeScope
   {
      [SerializeField] private SceneConfig _sceneConfig;
      
      protected override void Configure(IContainerBuilder builder)
      {
         builder.Register<ISceneService,SceneService>(Lifetime.Singleton).WithParameter(_sceneConfig);
         
         Debug.Log($"[{nameof(GameGlobalScope)}] Initialized.");
      }
      
   }
}
