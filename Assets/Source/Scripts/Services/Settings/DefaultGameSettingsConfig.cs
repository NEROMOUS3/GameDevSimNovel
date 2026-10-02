using UnityEngine;

namespace Source.Scripts.Services.Settings
{
    [CreateAssetMenu(fileName = "DefaultGameSettings", menuName = "Scriptable Objects/DefaultGameSettings")]
    public class DefaultGameSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public FullScreenMode WindowMode { get; private set; }
        [field: SerializeField] public Resolution ScreenResolution { get; private set; }
        [field: SerializeField] public int MaxFrameRate { get; private set; }
        [field: SerializeField] public bool Vsync { get; private set; }

        [field: SerializeField]
        [Range(0f, 1f)]
        public float GlobalVolume { get; private set; }

        [field: SerializeField]
        [Range(0f, 1f)]
        public float MusicVolume { get; private set; }

        [field: SerializeField]
        [Range(0f, 1f)]
        public float VFXVolume { get; private set; }

        [field: SerializeField] public bool NSFW { get; private set; }
    }
}