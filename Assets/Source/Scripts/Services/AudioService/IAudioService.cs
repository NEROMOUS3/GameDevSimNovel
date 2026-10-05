namespace Source.Scripts.Services.AudioService
{
    public interface IAudioService: IInitializationAwaiter
    {
        void ChangeAudioVolume(AudioType type, float volume);
        public void PlayAudio(string soundName);
        public void PauseAudio(string soundName);
        public void StopAudio(string soundName);
    }
}
