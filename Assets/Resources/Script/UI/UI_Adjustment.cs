using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Adjustment : UI_Popup
{
    public Text TotalMoney;
    public Button Close;
    public GameObject InventoryTreasure;
    public Transform Content;
    void Start()
    {
        Close.gameObject.SetActive(false);
        Init();
        StartCoroutine(Adjustment());

        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    IEnumerator Adjustment()
    {
        int addMoney = 0;
        foreach (StorableItem storable in GameManager.Instance.Inventory)
        {
            Treasure t = storable as Treasure;
            if (t == null)
                continue;
            addMoney += t.Worth;
            UI_Treasure ui_Treasure = Instantiate(InventoryTreasure).GetComponent<UI_Treasure>();
            ui_Treasure.transform.parent = Content;
            ui_Treasure.Init(t.Worth, t.Sprite);
            TotalMoney.text = $"รั ฑÝพื : \n {addMoney}";

            yield return new WaitForSeconds(.5f);
        }
        GameManager.Instance.Inventory.Clear();
        GameManager.Instance.CurrentMoney += addMoney;
        Close.gameObject.SetActive(true);
    }
}
