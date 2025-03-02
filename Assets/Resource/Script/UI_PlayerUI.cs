using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerUI : MonoBehaviour
{
    public UI_SliderController CorruptionSlider;
    public UI_SliderController CureSlider;

    public UI_PlayerHPBar PlayerHPBar;
    public UI_Score Score;

    public Image CurrentItem;
    public static Action<Sprite> UseItem;
    void Start()
    {
        CorruptionSlider.Init((int)(StageManager.Instance.GetMap().GetAreaCount() * .8f));
        CureSlider.Init((int)(StageManager.Instance.GetMap().GetAreaCount() * .8f));
        Score.Set(0);

        StageManager.Instance.ScoreChanged = SetScore;
        StageManager.Instance.CorruptedAreaCountChanged = SetCorruptionSliderValue;
        StageManager.Instance.CuredAreaCountChanged = SetCuredSliderValue;
        StageManager.Instance.PlayerHpChanged = SetPlayerHpBar;
        UseItem = SetImage;
        SetImage(null);
    }
    
    void SetScore(int score)
    {
        Score.Set(score);
    }
    void SetPlayerHpBar(int hpbar)
    {
        PlayerHPBar.Set(hpbar);
    }
    void SetCorruptionSliderValue(int value)
    {
        CorruptionSlider.Set(value);
    }
    void SetCuredSliderValue(int value)
    {
        CureSlider.Set(value);
    }
    public void SetImage(Sprite sprite)
    {
        if (sprite == null)
        {
            CurrentItem.color = new Color(1,1,1,0);
        }
        CurrentItem.color = new Color(1, 1, 1, 1);
        CurrentItem.sprite = sprite;
    }

}
