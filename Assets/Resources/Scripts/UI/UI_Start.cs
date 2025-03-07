using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Start : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(GameManager.Instance.CurrentStage);
        });

        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
