using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Services.AudioService
{
    public class AudioService: IAudioService, IInitializable
    {
        private Transform _audioContainer;
        private float _globalVolume;
        private float _musicVolume;
        private float _sfxVolume;
        private UniTaskCompletionSource _initialized =  new UniTaskCompletionSource();
        
        public UniTaskCompletionSource Initialized => _initialized;

        public AudioService(Transform audioContainer)
        {
            _audioContainer =  audioContainer;
            _globalVolume = 1.0f;
            _musicVolume = 1.0f;
            _sfxVolume = 1.0f;
        }
        
        public void ChangeAudioVolume(AudioType type, float volume)
        {
            switch (type)
            {
                case AudioType.GlobalVolume:
                    _globalVolume = volume;
                    break;
                case AudioType.MusicVolume:
                    _musicVolume = volume;
                    break;
                case AudioType.SfxVolume:
                    _sfxVolume = volume;
                    break;
            }
        }

        public void PlayAudio(string soundName)
        {
           
        }

        public void PauseAudio(string soundName)
        {
            
        }

        public void StopAudio(string soundName)
        {
            
        }

        public void Initialize()
        {
            _initialized.TrySetResult();
        }
    }
}
