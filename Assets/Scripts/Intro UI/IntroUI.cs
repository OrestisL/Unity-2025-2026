using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    public Button StartButton, QuitButton;

    private void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        QuitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame() 
    {
        Utilities.ChangeScene("Third Person", this);
    }

    private void QuitGame() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
