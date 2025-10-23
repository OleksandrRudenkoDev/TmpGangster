using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraAnchor;
        [SerializeField]
        private float _sensitivityX = 120f;
        [SerializeField]
        private float _sensitivityY = 100f;
        [SerializeField]
        private float _minVertical = -30f;
        [SerializeField]
        private float _maxVertical = 70f;

        private PlayerInputActions _playerInputActions;

        private Vector2 _input;
        private float _horizontal;
        private float _vertical;

        [Inject]
        private void Construct (PlayerInputActions input)
        {
            _playerInputActions = input;
            _playerInputActions.Camera.Move.performed += HandleMove;
            _playerInputActions.Camera.Move.canceled += HandleMove;
        }

        private void OnDestroy()
        {
            _playerInputActions.Camera.Move.performed -= HandleMove;
            _playerInputActions.Camera.Move.canceled -= HandleMove;
        }

        private void HandleMove (InputAction.CallbackContext context)
        {
            _input = context.ReadValue<Vector2>();
        }

        void LateUpdate()
        {
            _horizontal += _input.x * _sensitivityX * Time.deltaTime;
            _vertical -= _input.y * _sensitivityY * Time.deltaTime;
            _vertical = Mathf.Clamp(_vertical, _minVertical, _maxVertical);

            Quaternion rotation = Quaternion.Euler(_vertical, _horizontal, 0f);
            _cameraAnchor.rotation = rotation;
        }
    }
}