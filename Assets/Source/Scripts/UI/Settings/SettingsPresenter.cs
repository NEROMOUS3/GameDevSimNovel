using Source.Scripts.Services.Settings;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.UI.Settings
{
    public class SettingsPresenter: IInitializable
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly SettingsView _settingsView;

        [Inject]
        public SettingsPresenter(ISettingsProvider settingsProvider,  SettingsView settingsView)
        {
            
        }

        public void Initialize()
        {
           
        }
    }
}
