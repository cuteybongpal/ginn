using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UI_GameClear : MonoBehaviour
{
    public Button NextStage;
    void Start()
    {

        AddOnClickEvent(() =>
        {
            GameManager.Instance.CurrentStage++;
            SceneManager.LoadScene(GameManager.Instance.CurrentStage);
        }, NextStage);
    }
    void AddOnClickEvent(Action action, Button button)
    {
        button.onClick.AddListener(() =>
        {
            action.Invoke();
        });
    }
}
