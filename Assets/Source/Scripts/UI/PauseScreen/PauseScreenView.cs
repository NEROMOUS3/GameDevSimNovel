using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.PauseScreen
{
    public enum PauseScreenOption
    {
        Continue,
        Settings,
        MainMenu
    }

    public class PauseScreenView : MonoBehaviour
    {
        public event Action<PauseScreenOption> MenuActionPerformed;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backToMainMenuButton;
        
        public void Show()
        {
            SubscribeOnButtons();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            UnsubscribeFromButtons();
            gameObject.SetActive(false);
        }

        private void SubscribeOnButtons()
        {
            _continueButton.onClick.AddListener(OnContinueButton);
            _settingsButton.onClick.AddListener(OnSettingsButton);
            _backToMainMenuButton.onClick.AddListener(OnMainMenuButton);
        }
        
        private void UnsubscribeFromButtons()
        {
            _continueButton.onClick.RemoveListener(OnContinueButton);
            _settingsButton.onClick.RemoveListener(OnSettingsButton);
            _backToMainMenuButton.onClick.RemoveListener(OnMainMenuButton);
        }

        private void OnMainMenuButton()
        {
            MenuActionPerformed?.Invoke(PauseScreenOption.MainMenu);
        }

        private void OnSettingsButton()
        {
            MenuActionPerformed?.Invoke(PauseScreenOption.Settings);
        }

        private void OnContinueButton()
        {
            MenuActionPerformed?.Invoke(PauseScreenOption.Continue);
        }

        private void OnDestroy()
        {
            UnsubscribeFromButtons();
        }
    }
}