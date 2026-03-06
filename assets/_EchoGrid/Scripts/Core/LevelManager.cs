using UnityEngine;
using UnityEngine.SceneManagement;

namespace EchoGrid.Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Level Reloaded.");
        }

        public void LoadNextLevel()
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.Log("No more levels! Return to Main Menu.");
                SceneManager.LoadScene(0); // Assuming 0 is Main Menu
            }
        }
    }
}
