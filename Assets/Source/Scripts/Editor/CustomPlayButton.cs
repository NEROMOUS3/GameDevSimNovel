using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Source.Scripts.Editor
{
    [InitializeOnLoad]
    public class CustomPlayButton
    {
        private const string BOOT_SCENE_PATH = "Assets/Source/Scenes/BootScene.unity";
    
        static CustomPlayButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            if (GUILayout.Button(new GUIContent("▶ Boot", "Start from Boot scene"), EditorStyles.toolbarButton))
            {
                StartFromScene(BOOT_SCENE_PATH);
            }
        }

        static void StartFromScene(string scenePath)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            if (sceneAsset != null)
            {
                EditorSceneManager.playModeStartScene = sceneAsset;
                EditorApplication.isPlaying = true; 
            }
            else
            {
                Debug.LogError($"Scene not found: {scenePath}");
            }
        }
    }
}
