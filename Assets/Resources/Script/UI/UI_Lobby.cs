using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
    public Button StoreButton;
    public GameObject AdjustmentPrefab;
    public GameObject StorePrefab;
    public Text CoinText;
    protected override void Start()
    {
        base.Start();
        if (GameManager.Instance.Inventory.Count > 0)
        {
            Instantiate(AdjustmentPrefab);
        }
        StoreButton.onClick.AddListener(() =>
        {
            Instantiate(StorePrefab);
        });
        SetMoneyText(GameManager.Instance.CurrentMoney);

    }
    public void SetMoneyText(int money)
    {
        CoinText.text = money.ToString();
    }
}