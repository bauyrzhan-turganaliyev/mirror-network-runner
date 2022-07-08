using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace nmRunner
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Transform _camera;
        [SerializeField] private GameObject _cinemachineCamera;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _speed = 12f;
        [SerializeField] private float _turnSmoothTime = 0.1f;

        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private float _jumpHeight = 3f;
        [SerializeField] private LayerMask _groundMask;

        private Dash _dash;
        private Vector3 _velocity;
        private Vector3 _moveDir;
        private bool _isGrounded;
        private bool _isControl;

        private float _turnSmoothVelocity;


        public CharacterController Controller
        {
            get
            {
                return _controller;
            }
        }

        public Vector3 MoveDir
        {
            get
            {
                return _moveDir;
            }
        }
        public bool IsGrounded
        {
            get
            {
                return _isGrounded;
            }
        }
        public void Init(Dash dash)
        {
            _dash = dash;

            _dash.Init(this);

            _dash.OnDash += OnDashing;

            Cursor.lockState = CursorLockMode.Locked;

            _isControl = true;

            if (!isLocalPlayer)
            {
                _cinemachineCamera.SetActive(false);
            }
        }

        private void OnDashing(bool flag)
        {
            _isControl = !flag;
        }

        void Update()
        {
            if (!_isControl) return;

            // If we are not the main client, do not run this method
            if (!isLocalPlayer) return;
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                _moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                _controller.Move(_moveDir.normalized * _speed * Time.deltaTime);
            }



            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            _velocity.y += _gravity * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}
