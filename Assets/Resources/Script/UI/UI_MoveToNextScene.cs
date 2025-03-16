using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MoveToNextScene : UI_Popup
{
    public Button MoveToLobby;
    public Button Close;
    public Text text;
    public void Initialize(string Text, int SceneNum)
    {
        Init();
        text.text = Text;
        MoveToLobby.onClick.AddListener(() =>
        {
            GameManager.Instance.CurrentStage = SceneNum;
            Hide();
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
}
