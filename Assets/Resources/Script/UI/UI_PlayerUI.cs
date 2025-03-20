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
    public Text WeightText;
    public Image Inventory;

    public GameObject InventoryItemPrefab;
    public Image[] InventoryImages;
    Button[] buttons;

    public Collider AttackCollider;
    protected override void Start()
    {
        base.Start();
        OxygenSlider.maxValue = GameManager.Instance.PlayerMaxOxygenGage;

        SetOxygenGageValue(GameManager.Instance.PlayerMaxOxygenGage);

        InventoryImages = new Image[GameManager.Instance.MaxContainTreauseCount];
        buttons = new Button[GameManager.Instance.MaxContainTreauseCount];
        for (int i = 0; i < GameManager.Instance.MaxContainTreauseCount; i++)
        {
            GameObject go = Instantiate(InventoryItemPrefab);
            go.transform.SetParent(Inventory.transform);
            InventoryImages[i] = go.transform.GetChild(0).GetComponent<Image>();
            buttons[i] = go.transform.GetComponent<Button>();
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int a = i;
            Button button = buttons[i];
            if (button == null)
                Debug.Log("button is null");

            button.onClick.AddListener(() =>
            {
                if (GameManager.Instance.Inventory.Count <= a)
                    return;
                Item item = GameManager.Instance.Inventory[a] as Item;
                if (item == null)
                    return;
                item.UseItem();
                GameManager.Instance.Remove(a);
            });
        }
        StartCoroutine(A());
        WeightText.text = $"0 / {GameManager.Instance.MaxStorableWeight}kg";
        foreach (StorableItem item in GameManager.Instance.Inventory)
        {
            Treasure t = item as Treasure;
            Item item1 = item as Item; 
            if (t != null)
            {
                SetInventoryImage(t.Sprite);
            }
            else
            {
                SetInventoryImage(item1.ItemSprite);
            }
        }
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
    public void SetInventoryImage(Sprite sprite)
    {
        if (sprite == null) return;

        Image image = null;

        for (int i = 0; i < InventoryImages.Length; i++)
        {
            if (InventoryImages[i].sprite == null)
            {
                image = InventoryImages[i];
                break;
            }
        }
        int currentWeight = 0;

        foreach (StorableItem item in GameManager.Instance.Inventory)
        {
            Treasure t = item as Treasure;
            if (t == null)
                continue;
            currentWeight += t.Weight;
        }
        if (currentWeight > GameManager.Instance.MaxStorableWeight)
            WeightText.color = Color.red;
        WeightText.text = $"{currentWeight} / {GameManager.Instance.MaxStorableWeight}kg";
        image.sprite = sprite;
    }
    public void UseItem(int index)
    {
        InventoryImages[index].sprite = null;
    }
    
}
