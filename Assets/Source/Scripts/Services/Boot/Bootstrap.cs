using Cysharp.Threading.Tasks;
using Source.Scripts.Services.SceneService;
using Source.Scripts.UI.LoadingScreen;
using UnityEngine;
using VContainer;

namespace Source.Scripts.Services.Boot
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BootstrapConfig _bootConfig;
        private ISceneService _sceneService;
        private LoadingScreenProvider _loadingScreenProvider;

        [Inject]
        public void Configure(ISceneService sceneService, LoadingScreenProvider loadingScreenProvider)
        {
            _sceneService =  sceneService;
            _loadingScreenProvider =  loadingScreenProvider;
            Debug.Log($"[{nameof(Bootstrap)}] Configured.");
            Boot().Forget();
        }

        private async UniTaskVoid Boot()
        {
            Debug.Log($"[{nameof(Bootstrap)}] Initializing...");
            await _loadingScreenProvider.ShowLoadingScreen();
            ApplySettings();
            LoadMainMenu().Forget();
        }

        private async UniTaskVoid LoadMainMenu()
        {
           await _sceneService.ChangeScene(SceneType.GameLevel);
           _loadingScreenProvider.HideLoadingScreen().Forget();
        }

        private void ApplySettings()
        {
            Application.targetFrameRate = _bootConfig.initialFrameRate;
        }
    }
}
