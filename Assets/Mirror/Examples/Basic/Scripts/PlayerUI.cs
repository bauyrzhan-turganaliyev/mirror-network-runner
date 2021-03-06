using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mirror.Examples.Basic
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Child Text Objects")]
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private TMP_Text _playerScore;

        // This value can change as clients leave and join
        public void OnPlayerNumberChanged(byte newPlayerNumber)
        {
            _playerNameText.text = $"Player: {newPlayerNumber}";
        }

        public void OnPlayerDataChanged(ushort newPlayerScore)
        {
            // Show the data in the UI
            _playerScore.text = $"Data: {newPlayerScore}";
        }
    }
}
