using Source.Scripts.Services.SceneService;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Source.Scripts.Services.Boot
{
    public class Bootstrap : MonoBehaviour
    {
        
        [SerializeField] private BootstrapConfig _bootConfig;
        private ISceneService _sceneService;

        [Inject]
        public void Configure(ISceneService sceneService)
        {
            _sceneService =  sceneService;
            Debug.Log($"[{nameof(Bootstrap)}] Configured.");
            Boot();
        }

        private void Boot()
        {
            Debug.Log($"[{nameof(Bootstrap)}] Initializing...");
            Application.targetFrameRate = _bootConfig.initialFrameRate;
            LoadMainMenu();
        }

        private void LoadMainMenu()
        {
            /*var sceneName = SceneManager.GetSceneByBuildIndex(_bootConfig.targetSceneIndex);
            Debug.Log("Loading scene is "+sceneName);
            SceneManager.LoadScene(_bootConfig.targetSceneIndex);*/
            _sceneService.ChangeScene(SceneType.GameLevel);
        }
    }
}
