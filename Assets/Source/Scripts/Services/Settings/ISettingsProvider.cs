using UnityEngine;
using AudioType = Source.Scripts.Services.AudioService.AudioType;

namespace Source.Scripts.Services.Settings
{
    public interface ISettingsProvider : IInitializationAwaiter
    {
        void SaveSettings();
        void ResetSettings();
        void SetScreenSettings(Vector2Int resolution, FullScreenMode mode);
        void SetMaxFrameRate(int frameRate);
        void SetVsync(bool vsync);
        void SetVolume(AudioType audioType,float volume);
        void SetNSFW(bool enable);
    }
}
