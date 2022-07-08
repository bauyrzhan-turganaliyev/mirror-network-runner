using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nmRunner
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Dash _dash;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerNetwork _playerNetwork;

        private bool _isHit;

        public Action OnCollisionWithPlayer;
        private void Start()
        {
            _playerMovement.Init(_dash);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.TryGetComponent<Player>(out Player playerComponent))
            {
                if (_dash.IsDashing && !_isHit)
                {
                    _isHit = true;
                    _playerNetwork.AddScore();
                    StartCoroutine(HitReload(_dash.DashTimeReload));
                    playerComponent.OnCollisionWithPlayer?.Invoke();
                }
            }
        }

        private IEnumerator HitReload(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isHit = false;
        }
    }
}
