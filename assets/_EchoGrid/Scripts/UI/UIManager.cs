using UnityEngine;
using TMPro; // TextMeshPro support for modern Unity UI

namespace EchoGrid.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        
        [Header("HUD Elements")]
        [SerializeField] private TextMeshProUGUI statusText;

        public void ShowWinScreen()
        {
            winPanel.SetActive(true);
        }

        public void ShowLoseScreen()
        {
            losePanel.SetActive(true);
        }

        public void UpdateStatus(string message)
        {
            if (statusText != null) statusText.text = message;
        }

        public void OnRestartButtonClicked()
        {
            EchoGrid.Core.LevelManager.Instance.ReloadLevel();
        }
    }
}
