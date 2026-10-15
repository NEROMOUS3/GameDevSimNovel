using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Services.PauseHandler;
using Source.Scripts.Services.SceneService;
using Source.Scripts.UI.LoadingScreen;
using VContainer.Unity;

namespace Source.Scripts.UI.PauseScreen
{
    public class PauseScreenPresenter: IInitializable, IDisposable
    {
        private readonly ISceneService _sceneService;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly PauseScreenView _view;
        private readonly IPauseHandler _pauseHandler;

        public PauseScreenPresenter(PauseScreenView view,  ISceneService sceneService, LoadingScreenProvider loadingScreenProvider,  IPauseHandler pauseHandler)
        {
            _view =  view;
            _sceneService = sceneService;
            _loadingScreenProvider = loadingScreenProvider;
            _pauseHandler = pauseHandler;
        }

        public void Initialize()
        {
            _pauseHandler.PauseRequested += HandlePauseRequest;
            _view.MenuActionPerformed += HandlePauseScreenButtons;
        }

        private void HandlePauseScreenButtons(PauseScreenOption action)
        {
            switch (action)
            {
                case PauseScreenOption.Settings:
                {
                    break;
                }
                case PauseScreenOption.Continue:
                {
                    if (_pauseHandler.GetPauseState())
                    {
                        _pauseHandler.ChangePauseState(false);
                        _view.Hide();
                    }
                    break;
                }
                case PauseScreenOption.MainMenu:
                {
                    ExitToMainMenu().Forget();
                    break;
                }
            }
        }

        private void HandlePauseRequest()
        {
            if (_pauseHandler.GetPauseState() == false)
            {
                _pauseHandler.ChangePauseState(true);
                _view.Show();
            }
            else
            {
                _pauseHandler.ChangePauseState(false);
                _view.Hide();
            }
        }
        
        private async UniTaskVoid ExitToMainMenu()
        {
            await _loadingScreenProvider.GetAwaiter();
            await _loadingScreenProvider.ShowLoadingScreen();
            await _sceneService.ChangeScene(SceneType.MainMenu);
        }

        public void Dispose()
        {
            _pauseHandler.PauseRequested -= HandlePauseRequest;
            _view.MenuActionPerformed -= HandlePauseScreenButtons;
        }
    }
}
