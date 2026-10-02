using Source.Scripts.UI.Menu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.DI
{
    public class MainMenuScope : LifetimeScope
    {
        [SerializeField] private MainMenuView _mainMenuView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuPresenter>().WithParameter(_mainMenuView);
        }
    }
}
