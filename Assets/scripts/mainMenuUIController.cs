using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class mainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button QuitButton;

    void Start()
    {
        var root=GetComponent<UIDocument>().rootVisualElement;
        StartButton = root.Q<Button>("StartBT");
        QuitButton = root.Q<Button>("QuitBT");

        StartButton.clicked += startButtonPress;
        QuitButton.clicked += quitButtonPress; 
    }

    private void startButtonPress()
    {
        SceneManager.LoadScene(1);
    }

    private void quitButtonPress()
    {

    }
}
