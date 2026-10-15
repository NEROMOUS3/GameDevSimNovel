using System;

namespace Source.Scripts.Services.PauseHandler
{
    public interface IPauseHandler
    {
        public event Action PauseRequested;
        public event Action<bool> PauseStateChanged;
        public void RequestPause();
        public void ChangePauseState(bool pause);
        public bool GetPauseState();
    }
}
