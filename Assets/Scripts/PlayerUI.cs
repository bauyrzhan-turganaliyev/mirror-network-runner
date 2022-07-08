using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace nmRunner
{
    public class PlayerUI : MonoBehaviour
    {

        [Header("Child Text Objects")]
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private TMP_Text _playerScore;

        // This value can change as clients leave and join
        public void OnPlayerNumberChanged(int newPlayerNumber)
        {
            _playerNameText.text = $"Player: {newPlayerNumber}";
        }

        public void OnPlayerDataChanged(int newPlayerScore)
        {
            // Show the data in the UI
            _playerScore.text = $"Score: {newPlayerScore}";
        }
    }
}
