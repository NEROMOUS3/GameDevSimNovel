using Cysharp.Threading.Tasks;
using Source.Scripts.Services.GameSettings;
using Source.Scripts.Services.Settings.OptionsValue;

namespace Source.Scripts.Services.Settings
{
    public interface ISettingsProvider : IInitializationAwaiter
    {
      
        void ApplyOption<T>(OptionType type, T value) where T : IOptionValue;
        T GetOptionValue<T>(OptionType type) where T : IOptionValue;

        void GetSystemInfo();
    }
}
