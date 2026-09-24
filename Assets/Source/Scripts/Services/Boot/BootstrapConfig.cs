using UnityEditor;
using UnityEngine;

namespace Source.Scripts.Services.Boot
{
    [CreateAssetMenu(fileName = "BootstrapConfig", menuName = "Scriptable Objects/BootstrapConfig")]
    public class BootstrapConfig : ScriptableObject
    {
        public int initialFrameRate = 60;
        public int targetSceneIndex = 1;
    }
}
