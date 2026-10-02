using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action<MainMenuAction> MenuActionPerformed;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _quitButton;
        private bool _active;

        public void Enable()
        {
            _playButton.interactable = true;
            _quitButton.interactable = true;
            _active = true;
        }

        public void Disable()
        {
            _active = false;
            _playButton.interactable = true;
            _quitButton.interactable = true;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _continue.onClick.AddListener(OnContinueButtonClicked);
            _settings.onClick.AddListener(OnSettingsButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _continue.onClick.RemoveListener(OnContinueButtonClicked);
            _settings.onClick.RemoveListener(OnSettingsButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnSettingsButtonClicked()
        {
            if(!_active) return;
            MenuActionPerformed?.Invoke(MainMenuAction.Settings);
        }

        private void OnContinueButtonClicked()
        {
            if(!_active) return;
            MenuActionPerformed?.Invoke(MainMenuAction.Continue);
        }

        private void OnQuitButtonClicked()
        {
            if(!_active) return;
            MenuActionPerformed?.Invoke(MainMenuAction.Quit);
        }

        private void OnPlayButtonClicked()
        {
            if(!_active) return;
            MenuActionPerformed?.Invoke(MainMenuAction.Play);
        }
    }

    public enum MainMenuAction
    {
        Play,
        Continue,
        Settings,
        Quit
    }
}
