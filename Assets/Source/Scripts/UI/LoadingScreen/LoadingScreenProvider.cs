using Cysharp.Threading.Tasks;

namespace Source.Scripts.UI.LoadingScreen
{
    public class LoadingScreenProvider
    {
        private readonly LoadingScreenView _view;
        private UniTaskCompletionSource _awaiter;

        public UniTask GetAwaiter() => _awaiter.Task;

        public LoadingScreenProvider(LoadingScreenView view)
        {
            _view = view;
        }

        public async UniTask ShowLoadingScreen()
        {
            _awaiter = new UniTaskCompletionSource();
            await _view.Show();
            _awaiter.TrySetResult();
        }

        public async UniTask HideLoadingScreen()
        {
            _awaiter = new UniTaskCompletionSource();
            await _view.Hide();
            _awaiter.TrySetResult();
        }
    }
}