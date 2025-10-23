using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Extensions;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement"), SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private float _rotationSmoothTime = 0.1f;
        [SerializeField]
        private float _jumpForce = 5f;
        [SerializeField]
        private float _gravity = -9.81f;
        [SerializeField]
        private float _fallMultiplier = 2.5f;
        [SerializeField]
        private float _lowJumpMultiplier = 2f;

        [SerializeField]
        private float _sprintMultiplier = 1.6f;

        [Header("References"), SerializeField]
        private Transform _cameraTransform;
        [SerializeField]
        private CharacterController _controller;

        private PlayerInputActions _playerInputActions;

        private Vector2 _input;
        private Vector3 _velocity;
        private float _currentSpeed;
        private float _rotationVelocity;
        private bool _isGrounded;
        private bool _isSprinting;

        [Inject]
        private void Construct (PlayerInputActions input)
        {
            _playerInputActions = input;
            _playerInputActions.Move.Move.performed += HandleMove;
            _playerInputActions.Move.Move.canceled += HandleMove;

            _playerInputActions.Move.Jump.performed += HandleJump;

            _playerInputActions.Move.Sprint.started += HandleSprint;
            _playerInputActions.Move.Sprint.canceled += HandleSprint;
            _playerInputActions.Enable();
        }

        private void Update()
        {
            _isGrounded = _controller.isGrounded;

            Vector3 moveDir = new Vector3(_input.x, 0, _input.y);

            if (moveDir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationVelocity, _rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

                Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                _currentSpeed = _moveSpeed * (_isSprinting ? _sprintMultiplier : 1f);
                _controller.Move(move.normalized * _currentSpeed * Time.deltaTime);
            }

            if (_velocity.y < 0)
            {
                _velocity.y += _gravity * _fallMultiplier * Time.deltaTime;
            } else if (_velocity.y > 0 && !_playerInputActions.Move.Jump.IsPressed())
            {
                _velocity.y += _gravity * _lowJumpMultiplier * Time.deltaTime;
            } else
            {
                _velocity.y += _gravity * Time.deltaTime;
            }

            _controller.Move(_velocity * Time.deltaTime);
        }

        private void OnDestroy()
        {

            _playerInputActions.Move.Move.performed -= HandleMove;
            _playerInputActions.Move.Move.canceled -= HandleMove;

            _playerInputActions.Move.Jump.performed -= HandleJump;

            _playerInputActions.Move.Sprint.started -= HandleSprint;
            _playerInputActions.Move.Sprint.canceled -= HandleSprint;
        }

        private void HandleSprint (InputAction.CallbackContext context)
        {
            _isSprinting = context.started;
        }

        private void HandleJump (InputAction.CallbackContext context)
        {
            if (_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
            }
        }

        private void HandleMove (InputAction.CallbackContext context)
        {
            _input = context.ReadValue<Vector2>();
        }
    }
}