using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMangerScript : MonoBehaviour
{
    [SerializeField] private GameObject Pausepanel;
    private bool isPaused = false;

    private void Start()
    {
        Pausepanel.SetActive(false);
    }

    void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Mainmenu();
        }
    }

    void PauseGame()
    {
        // Set isPaused to true
        isPaused = true;

        // Pause the game by setting the time scale to 0
        //Time.timeScale = 0f;
        Pausepanel.SetActive (true);
        // Additional pause logic (e.g., show pause menu, disable player control)
        // ...
    }

   public void ResumeGame()
    {
        // Set isPaused to false
        isPaused = false;

        // Resume the game by setting the time scale back to 1
        //Time.timeScale = 1f;
        Pausepanel.SetActive(false );
        // Additional resume logic (e.g., hide pause menu, enable player control)
        // ...
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
