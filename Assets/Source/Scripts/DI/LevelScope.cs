using Source.Scripts.GamePlay.Level;
using Source.Scripts.Services.PauseHandler;
using Source.Scripts.UI.PauseScreen;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.DI
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private PauseScreenView _pauseScreenView;
        [SerializeField] private LevelController _levelContoller;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PauseScreenPresenter>()
                .WithParameter(_pauseScreenView)
                .WithParameter(_levelContoller);
            builder.Register<LevelPauseHandler>(Lifetime.Scoped).As<IPauseHandler>();
        }
    }
}
