using UnityEngine;

namespace Source.Scripts.Services.SavingService.SaveContainers
{
    public class SettingsSaveContainer : ISaveContainer
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