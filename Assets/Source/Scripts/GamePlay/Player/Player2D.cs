using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Scripts.GamePlay.Player
{
    public class Player2D : MonoBehaviour
    {
        [SerializeField] private Player2DConfig _config;
        [Space]
        [SerializeField] private InputActionReference _interactionInputReference;
        [SerializeField] private MovementComponent2D _movementComponent2D;
        [SerializeField] private InteractableFinder _finder;

        public void Enable()
        {
            _interactionInputReference.action.performed += Interact; 
            _movementComponent2D.Enable();
            _finder.Enable();
            _interactionInputReference.action.Enable();
        }
        
        public void Disable()
        {
            _movementComponent2D.Disable();
            _finder.Disable();
            _interactionInputReference.action.performed -= Interact;
            _interactionInputReference.action.Disable();
        }
        

        private void Interact(InputAction.CallbackContext obj)
        {
            Debug.Log("Interact" +obj.phase);
        }
    }
}
