using System;

namespace Source.Scripts.Services.PauseHandler
{
    public class LevelPauseHandler : IPauseHandler
    {
        public event Action PauseRequested;
        public event Action<bool> PauseStateChanged;

        private bool _isPaused = false;

        public bool GetPauseState() => _isPaused;
        public void RequestPause() => PauseRequested?.Invoke();

        public void ChangePauseState(bool pause)
        {
            _isPaused = pause;
            PauseStateChanged?.Invoke(_isPaused);
        }
    }
}