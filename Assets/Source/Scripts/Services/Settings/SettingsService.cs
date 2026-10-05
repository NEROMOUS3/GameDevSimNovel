using Cysharp.Threading.Tasks;
using Source.Scripts.Services.AudioService;
using Source.Scripts.Services.SavingService;
using Source.Scripts.Services.SavingService.SaveContainers;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using AudioType = Source.Scripts.Services.AudioService.AudioType;

namespace Source.Scripts.Services.Settings
{
    public class SettingsService : ISettingsProvider, IInitializable
    {
        private readonly ISavingService _savingService;
        private readonly IAudioService _audioService;
        private readonly DefaultGameSettingsConfig _defaultGameSettingsConfig;
        private SettingsSaveContainer _settingsSaveContainer;
        private UniTaskCompletionSource _isInitialized = new UniTaskCompletionSource();

        public UniTaskCompletionSource Initialized => _isInitialized;

        [Inject]
        public SettingsService(ISavingService savingService, DefaultGameSettingsConfig defaultGameSettingsConfig, IAudioService audioService)
        {
            _savingService = savingService;
            _audioService =  audioService;
            _defaultGameSettingsConfig = defaultGameSettingsConfig;
        }

        public void Initialize()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] initializing.");
            if (_savingService.CheckSaves(SavePath.GAME_SETTINGS))
                LoadSaveSettings();
            else
                ApplyDefaultSettings();
        }

        public void SaveSettings()
        {
            _savingService.Save(SavePath.GAME_SETTINGS, _settingsSaveContainer);
        }

        public void ResetSettings()
        {
            ApplyDefaultSettings();
        }

        public void SetScreenSettings(Vector2Int resolution, FullScreenMode mode)
        {
            _settingsSaveContainer.ScreenResolution = resolution;
            _settingsSaveContainer.WindowMode = mode;
            Screen.SetResolution(resolution.x, resolution.y, mode);
        }

        public void SetMaxFrameRate(int frameRate)
        {
            _settingsSaveContainer.MaxFrameRate = frameRate;
            Application.targetFrameRate = frameRate;
        }

        public void SetVsync(bool vsync)
        {
            _settingsSaveContainer.Vsync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
        }

        public void SetVolume(AudioType audioType, float volume)
        {
            _audioService.ChangeAudioVolume(audioType, volume);
            switch (audioType)
            {
                case AudioType.GlobalVolume:
                {
                    _settingsSaveContainer.GlobalVolume = volume;
                    break;
                }
                case AudioType.MusicVolume:
                {
                    _settingsSaveContainer.MusicVolume = volume;
                    break;
                }
                case AudioType.SfxVolume:
                {
                    _settingsSaveContainer.SfxVolume = volume;
                    break;
                }
            }
        }

        public void SetNSFW(bool enable)
        {
            _settingsSaveContainer.NSFW = enable;
        }

        private void ApplyDefaultSettings()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] {nameof(ApplyDefaultSettings)}.");
            _settingsSaveContainer = new SettingsSaveContainer();
            Vector2Int screenResolution;
            if (_defaultGameSettingsConfig.WindowMode == FullScreenMode.Windowed)
            {
                screenResolution = _defaultGameSettingsConfig.ScreenResolution;
            }
            else
            {
                screenResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
            }
            
            SetScreenSettings(screenResolution, _defaultGameSettingsConfig.WindowMode);
            SetMaxFrameRate(_defaultGameSettingsConfig.MaxFrameRate);
            SetVsync(_defaultGameSettingsConfig.Vsync);
            SetVolume(AudioType.GlobalVolume, _defaultGameSettingsConfig.GlobalVolume);
            SetVolume(AudioType.MusicVolume, _defaultGameSettingsConfig.MusicVolume);
            SetVolume(AudioType.SfxVolume, _defaultGameSettingsConfig.SfxVolume);
            SetNSFW(_defaultGameSettingsConfig.NSFW);

            _savingService.Save(SavePath.GAME_SETTINGS, _settingsSaveContainer);
            _isInitialized.TrySetResult();
        }

        private void LoadSaveSettings()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] {nameof(LoadSaveSettings)}.");
            if (!_savingService.Load(SavePath.GAME_SETTINGS, _settingsSaveContainer))
            {
                ApplyDefaultSettings();
                return;
            }

            SetScreenSettings(_settingsSaveContainer.ScreenResolution, _settingsSaveContainer.WindowMode);
            SetMaxFrameRate(_settingsSaveContainer.MaxFrameRate);
            SetVsync(_settingsSaveContainer.Vsync);
            SetVolume(AudioType.GlobalVolume, _settingsSaveContainer.GlobalVolume);
            SetVolume(AudioType.MusicVolume, _settingsSaveContainer.MusicVolume);
            SetVolume(AudioType.SfxVolume, _settingsSaveContainer.SfxVolume);
            SetNSFW(_settingsSaveContainer.NSFW);

            _isInitialized.TrySetResult();
        }
    }
}