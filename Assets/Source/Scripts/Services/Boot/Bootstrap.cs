using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Services.Boot
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BootstrapConfig _bootConfig;
        
        private void Awake()
        {
            Boot();
        }

        private void Boot()
        {
            Application.targetFrameRate = _bootConfig.initialFrameRate;
            LoadMainMenu();
        }

        private void LoadMainMenu()
        {
            var sceneName = SceneManager.GetSceneByBuildIndex(_bootConfig.targetSceneIndex);
            Debug.Log("Loading scene is "+sceneName);
            SceneManager.LoadScene(_bootConfig.targetSceneIndex);
        }
    }
}
