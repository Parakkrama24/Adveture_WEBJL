using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUiNEW : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;    
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
      Application.Quit();
    }
}
