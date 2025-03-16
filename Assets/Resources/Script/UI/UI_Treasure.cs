using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Treasure : MonoBehaviour
{
    public Text PriceText;
    public Image TreasureImage;
    public void Init(int price, Sprite sprite)
    {
        TreasureImage.sprite = sprite;
        PriceText.text = $"{price}+";
    }
}
