using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Services.SceneService
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Scriptable Objects/SceneConfig")]
    public class SceneConfig : ScriptableObject
    {
        [SerializeField] private List<SceneReference> _scenes = new List<SceneReference>();

        public bool TryGetSceneAsset(SceneType type, out string scenePath)
        {
            scenePath = default;
            if (_scenes == null || _scenes.Count == 0)
                return false;

            var sceneData = _scenes.FirstOrDefault(s => s.SceneType == type);
            if (sceneData == null)
                return false;

            scenePath = sceneData.ScenePath;
            return true;
        }

        [Serializable]
        private class SceneReference
        {
            public SceneType SceneType;
            public Object SceneAsset;
            public string ScenePath;
        }
        
#if EDITOR
        private void OnValidate()
        {
            Debug.Log("OnValidate");
            foreach (var scene in _scenes)
            {
                var assetPath = AssetDatabase.GetAssetPath(scene.SceneAsset);
                scene.ScenePath = assetPath;
            }
        }
#endif
    }
}