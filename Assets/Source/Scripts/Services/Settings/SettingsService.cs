using System.Collections.Generic;
using System.Linq;
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

        private DeviceData _deviceData;
        private UniTaskCompletionSource _isInitialized = new UniTaskCompletionSource();

        public UniTaskCompletionSource Initialized => _isInitialized;

        public DeviceData GetDeviceData() => _deviceData;

        public SettingData GetCurrentSettings() => _settingsSaveContainer.Data;

        [Inject]
        public SettingsService(ISavingService savingService, DefaultGameSettingsConfig defaultGameSettingsConfig,
            IAudioService audioService)
        {
            _savingService = savingService;
            _audioService = audioService;
            _defaultGameSettingsConfig = defaultGameSettingsConfig;
        }

        public void Initialize()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] initializing.");
            InitializeDeviceData();
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
            _settingsSaveContainer.Data.ScreenResolution = resolution;
            _settingsSaveContainer.Data.WindowMode = mode;
            Screen.SetResolution(resolution.x, resolution.y, mode);
        }

        public void SetMaxFrameRate(int frameRate)
        {
            _settingsSaveContainer.Data.MaxFrameRate = frameRate;
            Application.targetFrameRate = frameRate;
        }

        public void SetVsync(bool vsync)
        {
            _settingsSaveContainer.Data.Vsync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
        }

        public void SetVolume(AudioType audioType, float volume)
        {
            _audioService.ChangeAudioVolume(audioType, volume);
            switch (audioType)
            {
                case AudioType.GlobalVolume:
                {
                    _settingsSaveContainer.Data.GlobalVolume = volume;
                    break;
                }
                case AudioType.MusicVolume:
                {
                    _settingsSaveContainer.Data.MusicVolume = volume;
                    break;
                }
                case AudioType.SfxVolume:
                {
                    _settingsSaveContainer.Data.SfxVolume = volume;
                    break;
                }
            }
        }

        public void SetNSFW(bool enable)
        {
            _settingsSaveContainer.Data.NSFW = enable;
        }

        private void InitializeDeviceData()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] {nameof(InitializeDeviceData)}.");

            var availableScreenRes = new HashSet<Vector2Int>();
            var screenResolutionSet = new HashSet<Vector2Int>();
            var frameRateSet = new HashSet<double>();
            
            foreach (var res in _defaultGameSettingsConfig.AvailableScreenResolution)
            {
                availableScreenRes.Add(res);
            }
      
            foreach (var data in Screen.resolutions)
            {
                var screenRes = new Vector2Int(data.width, data.height);
                if(availableScreenRes.Contains(screenRes))
                    screenResolutionSet.Add(screenRes);
                if(data.refreshRateRatio.value>= _defaultGameSettingsConfig.MinRefreshRate)
                    frameRateSet.Add(data.refreshRateRatio.value);
            }

            _deviceData = new DeviceData()
            {
                ScreenResolution = screenResolutionSet.ToArray(),
                RefreshRate = frameRateSet.ToArray(),
            };

            Debug.Log($"Available screen resolutions = {_deviceData.ScreenResolution.Length}");
            foreach (var res in _deviceData.ScreenResolution)
            {
                Debug.Log($"Resolutions = {res}");
            }
            
            Debug.Log($"Available screen refreshRAte = {_deviceData.RefreshRate.Length}");
            foreach (var res in _deviceData.RefreshRate)
            {
                Debug.Log($"RefreshRate = {res}hz");
            }
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

            SaveSettings();
            FinishInitialization().Forget();
        }

        private void LoadSaveSettings()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] {nameof(LoadSaveSettings)}.");
            if (!_savingService.Load(SavePath.GAME_SETTINGS, _settingsSaveContainer))
            {
                ApplyDefaultSettings();
                return;
            }

            SetScreenSettings(_settingsSaveContainer.Data.ScreenResolution, _settingsSaveContainer.Data.WindowMode);
            SetMaxFrameRate(_settingsSaveContainer.Data.MaxFrameRate);
            SetVsync(_settingsSaveContainer.Data.Vsync);
            SetVolume(AudioType.GlobalVolume, _settingsSaveContainer.Data.GlobalVolume);
            SetVolume(AudioType.MusicVolume, _settingsSaveContainer.Data.MusicVolume);
            SetVolume(AudioType.SfxVolume, _settingsSaveContainer.Data.SfxVolume);
            SetNSFW(_settingsSaveContainer.Data.NSFW);

            FinishInitialization().Forget();
        }

        private async UniTaskVoid FinishInitialization()
        {
            await UniTask.WaitForFixedUpdate();
            _isInitialized.TrySetResult();
        }
    }
}