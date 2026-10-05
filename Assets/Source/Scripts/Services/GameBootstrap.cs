using Cysharp.Threading.Tasks;
using Source.Scripts.Services.AudioService;
using Source.Scripts.Services.SavingService;
using Source.Scripts.Services.SceneService;
using Source.Scripts.Services.Settings;
using Source.Scripts.UI.LoadingScreen;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.Services
{
    public class GameBootstrap : IInitializable
    {
        private readonly ISceneService _sceneService;
        private readonly ISavingService _savingService;
        private readonly IAudioService _audioService;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly ISettingsProvider _settingsProvider;

        [Inject]
        public GameBootstrap(ISceneService sceneService, LoadingScreenProvider loadingScreenProvider,
            ISettingsProvider settingsProvider, ISavingService savingService, IAudioService audioService)
        {
            _savingService = savingService;
            _sceneService = sceneService;
            _loadingScreenProvider = loadingScreenProvider;
            _settingsProvider = settingsProvider;
            _audioService = audioService;
        }
        
        public void Initialize()
        {
            Debug.Log($"[{nameof(GameBootstrap)}] {nameof(Initialize)}");
            Boot().Forget();
        }

        private async UniTaskVoid Boot()
        {
            await _savingService.Initialized.Task;
            await _audioService.Initialized.Task;
            await _settingsProvider.Initialized.Task;
            await _loadingScreenProvider.ShowLoadingScreen();
            Debug.Log($"[{nameof(GameBootstrap)}] Boot complete.");
            LoadMainMenu().Forget();
        }

        private async UniTaskVoid LoadMainMenu()
        {
            await _sceneService.ChangeScene(SceneType.MainMenu);
        }
    }
}