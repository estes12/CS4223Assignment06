using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private bool isPaused;
    public GameObject pauseMenu;

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame() // called from ESC
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
        void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC key pauses AND resumes
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }
}
