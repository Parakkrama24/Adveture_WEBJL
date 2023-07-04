using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUiNEW : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
      Application.Quit();
    }
}
