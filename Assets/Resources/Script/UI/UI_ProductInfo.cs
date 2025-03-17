using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProductInfo : UI_Popup
{
    public Image ProductImage;
    public Text Price;
    public Button BuyButton;
    public Text ProductName;

    public Product product;

    public void Initialize(Product product, UI_Shop shopUI)
    {
        ProductImage.sprite = product.Sprite;
        Price.text = $"{product.Price}";
        ProductName.text = $"{product.Name}({product.UpGrade+1}LV)";
        if (!product.IsBuy)
        {
            BuyButton.onClick.AddListener(() =>
            {
                product.Action.Invoke(product);
                shopUI.UpdateProduct();
            });
        }
        else
        {
            BuyButton.GetComponentInChildren<Text>().color = Color.white;
            BuyButton.GetComponentInChildren<Text>().text = "∏∏∑æ¿‘¥œ¥Ÿ.";
            BuyButton.GetComponent<Image>().color = new Color(.3f,.3f,.3f);
        }
    }
}
