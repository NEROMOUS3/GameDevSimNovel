using Cysharp.Threading.Tasks;
using Source.Scripts.GamePlay.Player;
using Source.Scripts.UI.LoadingScreen;
using UnityEngine;
using VContainer;

namespace Source.Scripts.GamePlay.Level
{
   public class OfficeLevel : MonoBehaviour
   {
      [SerializeField] private Player2D _playerPrefab;
      [SerializeField] private Transform _startPosition;
      
      private LoadingScreenProvider  _loadingScreenProvider;

      [Inject]
      public void InjectDependencies(LoadingScreenProvider loadingScreenProvider)
      {
         _loadingScreenProvider = loadingScreenProvider;
      }

      public void Start()
      {
         EnabLeLevel().Forget();
      }

      private async UniTaskVoid EnabLeLevel()
      {
         await _loadingScreenProvider.GetAwaiter();
         await _loadingScreenProvider.HideLoadingScreen();
      }
   }
}
