using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Services.SavingService
{
    public class SavingService: ISavingService, IInitializable
    {
        private UniTaskCompletionSource _isInitialized = new UniTaskCompletionSource();
        public UniTaskCompletionSource Initialized => _isInitialized;
        
        public void Initialize()
        {
            Debug.Log($"[{nameof(ISavingService)}] initializing.");
            _isInitialized.TrySetResult();
        }
    }
}
