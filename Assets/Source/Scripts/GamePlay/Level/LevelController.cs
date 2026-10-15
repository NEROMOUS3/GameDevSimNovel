using Cysharp.Threading.Tasks;
using Source.Scripts.GamePlay.Player;
using Source.Scripts.Services.PauseHandler;
using Source.Scripts.UI.LoadingScreen;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Source.Scripts.GamePlay.Level
{
   public class LevelController : MonoBehaviour
   {
      [SerializeField] private InputActionReference _pauseActionReference;
      [SerializeField] private Player2D _playerPrefab;
      [SerializeField] private Transform _startPosition;
      
      private bool _isPaused = false;
      private LoadingScreenProvider  _loadingScreenProvider;
      private IPauseHandler _pauseHandler;
      private Player2D _player;

      [Inject]
      public void InjectDependencies(LoadingScreenProvider loadingScreenProvider, IPauseHandler pauseHandler)
      {
         _loadingScreenProvider = loadingScreenProvider;
         _pauseHandler = pauseHandler;
      }

      public void Start()
      {
         _pauseHandler.PauseStateChanged += HandlePauseChanged;
         _pauseActionReference.action.performed += HandlePauseRequest;
         EnableLevel().Forget();
      }

      private void HandlePauseChanged(bool state)
      {
         if ( state == false)
            _player.Enable();
         else
            _player.Disable();
      }

      private void HandlePauseRequest(InputAction.CallbackContext obj)
      {
         _pauseHandler.RequestPause();
      }

      private async UniTaskVoid EnableLevel()
      {
         _player = Instantiate(_playerPrefab, _startPosition);
         await _loadingScreenProvider.GetAwaiter();
         await _loadingScreenProvider.HideLoadingScreen();
         _pauseActionReference.action.Enable();
         _player.Enable();
      }
   }
}
