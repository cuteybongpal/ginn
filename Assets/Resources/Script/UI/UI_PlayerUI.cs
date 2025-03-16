using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerUI : UI_Base
{
    public GameObject[] Hps;
    public Slider OxygenSlider;
    public Text OxygenText;
    public Image Inventory;

    public GameObject InventoryItemPrefab;
    public Image[] InventoryImages;

    protected override void Start()
    {
        base.Start();
        OxygenSlider.maxValue = GameManager.Instance.PlayerMaxOxygenGage;

        SetOxygenGageValue(GameManager.Instance.PlayerMaxOxygenGage);

        InventoryImages = new Image[GameManager.Instance.MaxContainTreauseCount];
        for (int i = 0; i < GameManager.Instance.MaxContainTreauseCount; i++)
        {
            GameObject go = Instantiate(InventoryItemPrefab);
            go.transform.SetParent(Inventory.transform);
            InventoryImages[i] = go.transform.GetChild(0).GetComponent<Image>();
        }
        StartCoroutine(A());
    }

    IEnumerator A()
    {
        yield return null;
        Inventory.rectTransform.anchoredPosition = new Vector3(-Inventory.rectTransform.rect.width / 2, -Inventory.rectTransform.rect.height / 2, 0);
    }

    public void SetHPBar(int hp)
    {
        for (int i = 0; i < Hps.Length; i++)
        {
            Hps[i].SetActive(true);
        }
        for (int i = hp; i < Hps.Length; i++)
        {
            Hps[i].SetActive(false);
        }
    }
    public void SetOxygenGageValue(int value)
    {
        OxygenSlider.value = value;
        OxygenText.text = $"현재 남은 산소량 : {value}";
    }
    public void TakeTreasure(Treasure treasure)
    {
        if (treasure == null) return;

        Image image = null;

        for (int i = 0; i < InventoryImages.Length; i++)
        {
            if (InventoryImages[i].sprite == null)
            {
                image = InventoryImages[i];
                break;
            }
        }
        if (image != null)
            image.sprite = treasure.Sprite;
    }
}
