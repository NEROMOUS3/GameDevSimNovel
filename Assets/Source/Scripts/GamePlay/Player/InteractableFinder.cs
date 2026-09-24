using UnityEngine;

namespace Source.Scripts.GamePlay.Player
{
    public class InteractableFinder : MonoBehaviour
    {
        [SerializeField] private Collider2D _interactableTrigger;

        public void Enable()
        {
            _interactableTrigger.enabled = true;
        }
        
        public void Disable()
        {
            _interactableTrigger.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}
