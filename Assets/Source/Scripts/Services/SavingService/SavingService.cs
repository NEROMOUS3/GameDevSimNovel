using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Services.SavingService
{
    public class SavingService : ISavingService, IInitializable
    {
        private UniTaskCompletionSource _isInitialized = new UniTaskCompletionSource();
        private Dictionary<string, ISaveContainer> _containers = new Dictionary<string, ISaveContainer>();
        
        public UniTaskCompletionSource Initialized => _isInitialized;

        public void Initialize()
        {
            Debug.Log($"[{nameof(ISavingService)}] initializing.");
            _containers = new Dictionary<string, ISaveContainer>();
            _isInitialized.TrySetResult();
        }

        public bool CheckSaves(string path)
        {
            return _containers.ContainsKey(path);
        }

        public void Save(string path, ISaveContainer container)
        {
            if (_containers.ContainsKey(path))
            {
                _containers[path] = container;
            }
            else
            {
                _containers.TryAdd(path, container);
            }
        }
        
        public bool Load<T>(string path, T outContainer) where T : ISaveContainer
        {
            if (!_containers.TryGetValue(path, out var saveContainer)) return false;
            if (saveContainer is not T value) return false;
            outContainer = value;
            return true;
        }
    }
}