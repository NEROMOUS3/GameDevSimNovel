using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Services.SceneService;
using Source.Scripts.UI.LoadingScreen;
using UnityEngine;
using IInitializable = VContainer.Unity.IInitializable;

namespace Source.Scripts.UI.Menu
{
    public class MainMenuPresenter: IInitializable, IDisposable
    {
        private readonly MainMenuView _view;
        private ISceneService _sceneService;
        private LoadingScreenProvider _loadingScreenProvider;
        
        public MainMenuPresenter(MainMenuView view,  ISceneService sceneService,  LoadingScreenProvider loadingScreenProvider)
        {
            _view = view;
            _sceneService = sceneService;
            _loadingScreenProvider = loadingScreenProvider;
        }

        public void Initialize()
        {
            Debug.Log($"{nameof(MainMenuPresenter)} initializing.");
            _view.MenuActionPerformed += HandleAction;
            EnableMainMenu().Forget();
        }

        private async UniTaskVoid EnableMainMenu()
        {
            await _loadingScreenProvider.HideLoadingScreen();
            _view.Enable();
        }
        
        public void Dispose()
        {
            _view.MenuActionPerformed -= HandleAction;
        }

        private void HandleAction(MainMenuAction actionType)
        {
            switch (actionType)
            {
                case MainMenuAction.Quit:
                {
                    Application.Quit();
                    break;
                }
                case MainMenuAction.Play:
                {
                    _view.Disable();
                    LoadGameScene().Forget();
                    break;
                }
            }
        }

        private async UniTaskVoid LoadGameScene()
        {
            await _loadingScreenProvider.ShowLoadingScreen();
            await _sceneService.ChangeScene(SceneType.GameLevel);
        }
    }
}
