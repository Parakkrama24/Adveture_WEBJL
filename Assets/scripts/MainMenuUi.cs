using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Time.timeScale);

       if( Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Input2");
            play();
        }
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    public void play()
    {
        Debug.Log("Input2");
        SceneManager.LoadScene(1);

    }
    public void Quit() {
    Application.Quit();
    
    }
    
}
