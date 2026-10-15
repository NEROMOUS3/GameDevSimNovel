using UnityEngine;

namespace Source.Scripts.Services.Settings
{
    public struct SettingData
    {
        public FullScreenMode WindowMode;
        public Vector2Int ScreenResolution;
        public int MaxFrameRate;
        public bool Vsync;
        public float GlobalVolume;
        public float MusicVolume;
        public float SfxVolume;
        public bool NSFW;
    }
}
