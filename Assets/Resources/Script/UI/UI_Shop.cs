using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        UpdateProduct();
    }
    public void UpdateProduct()
    {
        List<Product> products = DataManager.Instance.Products;
        foreach (Transform t in LayoutGroup.transform)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < products.Count; i++)
        {
            GameObject go = Instantiate(UI_ProductInfoPrefab);
            go.transform.parent = LayoutGroup.transform;
            go.GetComponent<UI_ProductInfo>().Initialize(products[i], this);
        }
    }
    
}
