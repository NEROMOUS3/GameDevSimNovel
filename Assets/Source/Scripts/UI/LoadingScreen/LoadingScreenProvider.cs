using Cysharp.Threading.Tasks;

namespace Source.Scripts.UI.LoadingScreen
{
    public class LoadingScreenProvider
    {
        private readonly LoadingScreenView _view;

        public LoadingScreenProvider(LoadingScreenView view)
        {
            _view =  view;
        }

        public async UniTask ShowLoadingScreen()
        {
            await _view.Show();
        }

        public async UniTask HideLoadingScreen()
        {
            await _view.Hide();
        }
    }
}
