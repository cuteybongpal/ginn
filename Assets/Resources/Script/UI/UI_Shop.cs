using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    public Button CloseButton;
    public GameObject UI_ProductInfoPrefab;
    public GameObject LayoutGroup;
    void Start()
    {
        Init();
        CloseButton.onClick.AddListener(() =>
        {
            Hide();
        });
        List<Product> products = DataManager.Instance.Products;
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].IsBuy)
                continue;
            GameObject go = Instantiate(UI_ProductInfoPrefab);
            go.transform.parent = LayoutGroup.transform;
            go.GetComponent<UI_ProductInfo>().Initialize(products[i]);
        }
    }
    
}
