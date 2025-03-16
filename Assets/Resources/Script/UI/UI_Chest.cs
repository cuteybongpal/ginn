using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Chest : UI_Popup
{
    public Image TreasureSprite;
    public Text TreasureName;
    public Text TreasureWeight;
    public Text TreasurePrice;
    public Button TakeButton;
    public Button CloseButton;
    public void Init(Treasure treasure, TreasueChest chest)
    {
        Init();
        TreasureName.text = treasure.Name;
        TreasureWeight.text = $"���� : {treasure.Weight} kg";
        TreasurePrice.text = $"���� : {treasure.Worth} ��";
        TreasureSprite.sprite = treasure.Sprite;

        TakeButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.Add(new Treasure(treasure)))
            {
                chest.Take();
                Hide();
            }

        });

        CloseButton.onClick.AddListener(() => 
        {
            chest.Close();
            Hide();
        });
    }


}
