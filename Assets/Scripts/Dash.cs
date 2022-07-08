using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nmRunner
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashTime;
        [SerializeField] private float _dashTimeReload;
        [SerializeField] private bool _isDashing;
        [SerializeField] private bool _dashInAir;
        public Action<bool> OnDash;

        private PlayerMovement _moveScript;

        public bool IsDashing
        {
            get
            {
                return _isDashing;
            }
        }

        public float DashTimeReload
        {
            get
            {
                return _dashTimeReload;
            }
        }
        public void Init(PlayerMovement moveScript) {
            _moveScript = moveScript;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isDashing)
            {
                if (!_dashInAir && !_moveScript.IsGrounded) return;

                _isDashing = true;
                StartCoroutine(Dashing());
            }
        }

        private IEnumerator Dashing()
        {
            OnDash?.Invoke(true);
            StartCoroutine(StopDashing());

            float startTime = Time.time;

            while (Time.time < startTime + _dashTime)
            {
                _moveScript.Controller.Move(_moveScript.MoveDir * _dashSpeed * Time.deltaTime);
                yield return null;
            }

        }

        private IEnumerator StopDashing()
        {
            yield return new WaitForSeconds(_dashTime);
            OnDash?.Invoke(false);
            yield return new WaitForSeconds(_dashTimeReload);
            _isDashing = false;
        }
    }
}
