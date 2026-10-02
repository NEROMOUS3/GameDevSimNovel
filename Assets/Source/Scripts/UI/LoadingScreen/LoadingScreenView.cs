using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.UI.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _speed;
        [SerializeField] private Ease _showEase = Ease.Linear;
        [SerializeField] private Ease _hideEase = Ease.Linear;
        private Tweener _animationTween;
        
        public async UniTask Show()
        {
            _animationTween?.Kill();
            gameObject.SetActive(true);
            _animationTween = _canvasGroup.DOFade(1, _speed).SetEase(_showEase);
            await _animationTween.AsyncWaitForCompletion();
        }

        public async UniTask Hide()
        {
            _animationTween?.Kill();
            _animationTween = _canvasGroup.DOFade(0, _speed).SetEase(_hideEase);
            await _animationTween.AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }
    }
}
