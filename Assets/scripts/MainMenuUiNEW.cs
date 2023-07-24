using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUiNEW : MonoBehaviour
{
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter)) 
         SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        
    }
   
}
