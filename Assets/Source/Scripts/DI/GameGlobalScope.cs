using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.DI
{
   public class GameGlobalScope : LifetimeScope
   {
      protected override void Configure(IContainerBuilder builder)
      {
         Debug.Log($"[{nameof(GameGlobalScope)}] Initialized.");
      }
   }
}
