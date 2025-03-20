using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : UI_Base
{
    public Button Try;
    protected override void Start()
    {
        base.Start();
        Try.onClick.AddListener(() =>
        {
            GameManager.Instance.CurrentStage = GameManager.Instance.CurrentStage;
        });
    }
}
