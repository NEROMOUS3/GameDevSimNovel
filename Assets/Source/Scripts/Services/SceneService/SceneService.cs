using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Services.SceneService
{
    public class SceneService : ISceneService
    {
        private readonly SceneConfig _sceneConfig;
        
        public SceneService(SceneConfig sceneConfig)
        {
            _sceneConfig =  sceneConfig;
        }
        
        public async UniTask ChangeScene(SceneType type)
        {
            if (_sceneConfig.TryGetSceneAsset(type, out var scenePath) == false)
            {
                Debug.LogWarning($"Scene with type {type} not exist!");
                return;
            }

            await SceneManager.LoadSceneAsync(scenePath);
        }
    }
}
