using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Services.GameSettings;
using Source.Scripts.Services.SavingService;
using Source.Scripts.Services.Settings.OptionsValue;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.Services.Settings
{
    public class SettingsService : ISettingsProvider, IInitializable
    {
        private readonly ISavingService _savingService;
        private readonly DefaultGameSettingsConfig _defaultGameSettingsConfig;

        private Dictionary<OptionType, IOptionValue> _options;
        private UniTaskCompletionSource _isInitialized = new UniTaskCompletionSource();
        public UniTaskCompletionSource Initialized => _isInitialized;

        [Inject]
        public SettingsService(ISavingService savingService, DefaultGameSettingsConfig defaultGameSettingsConfig)
        {
            _savingService =  savingService;
            _defaultGameSettingsConfig = defaultGameSettingsConfig;
        }
        
        public void Initialize()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] initializing.");
            _options = new Dictionary<OptionType, IOptionValue>()
            {
                { OptionType.WindowMode, new BoolOptionValue() },
                { OptionType.NSFW, new BoolOptionValue() },
                { OptionType.GlobalVolume, new FloatOptionValue() },
                { OptionType.VFXVolume, new FloatOptionValue() },
                { OptionType.MusicVolume, new FloatOptionValue() },
            };
            // if saves ApplySaves()
            //else 
            ApplyDefaultSettings();
        }
        
        public void ApplyOption<T>(OptionType type, T value) where T : IOptionValue
        {
            if (_options.TryGetValue(type, out IOptionValue option))
            {
                if (option is T boolOption)
                {
                    option = value;
                    ApplyValue(type);
                }
            }
        }

        public T GetOptionValue<T>(OptionType type) where T : IOptionValue
        {
            var possibleValue = _options.GetValueOrDefault(type);
            if (possibleValue is T targetOptionValue)
                return targetOptionValue;

            return default;
        }

        public void GetSystemInfo()
        {
            throw new System.NotImplementedException();
        }


        private void ApplyValue(OptionType type)
        {
            switch (type)
            {
            }
        }

        private void ApplyDefaultSettings()
        {
            Debug.Log($"[{nameof(ISettingsProvider)}] {nameof(ApplyDefaultSettings)}.");
            Application.targetFrameRate = _defaultGameSettingsConfig.MaxFrameRate;
            _isInitialized.TrySetResult();
        }

        private void ApplySaves()
        {
            _isInitialized.TrySetResult();
        }
    }
}