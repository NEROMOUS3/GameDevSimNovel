using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Scripts.GamePlay.Player
{
    public class MovementComponent2D : MonoBehaviour
    {
        [SerializeField] private InputActionReference _movementInputReference;
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private float _movementSpeedMultiplier;
        private Vector2 _movementDirection;

        public void Enable()
        {
            _movementInputReference.action.Enable();
        }

        public void Disable()
        {
            _movementInputReference.action.Disable();
        }

        private void Update()
        {
            _movementDirection = _movementInputReference.action.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + _movementDirection * _movementSpeedMultiplier * Time.fixedDeltaTime);
        }
    }
}
