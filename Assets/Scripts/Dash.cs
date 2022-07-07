using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nmRunner
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _moveScript;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashTime;
        [SerializeField] private float _dashTimeReload;
        [SerializeField] private bool _isDashing;
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isDashing)
            {
                StartCoroutine(Dashing());
            }
        }

        private IEnumerator Dashing()
        {
            float startTime = Time.time;

            while (Time.time < startTime + _dashTime)
            {
                _moveScript.Controller.Move(_moveScript.MoveDir * _dashSpeed * Time.deltaTime);
                _isDashing = true;
                StartCoroutine(StopDashing());
                yield return null;
            }

        }

        private IEnumerator StopDashing()
        {
            yield return new WaitForSeconds(_dashTimeReload);
            _isDashing = false;
        }
    }
}
