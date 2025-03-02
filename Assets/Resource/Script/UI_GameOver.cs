using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    public Button PlayAgain;
    public Button Quit;
    void Start()
    {
        AddOnClickEvent(() =>
        {
            Debug.Log(GameManager.Instance.CurrentStage);
            SceneManager.LoadScene(GameManager.Instance.CurrentStage);
        }, PlayAgain);
        AddOnClickEvent(() =>
        {
            SceneManager.LoadScene(0);
        }, Quit);
    }

    void AddOnClickEvent(Action action, Button button)
    {
        button.onClick.AddListener(() =>
        {
            action.Invoke();
        });
    }
}
