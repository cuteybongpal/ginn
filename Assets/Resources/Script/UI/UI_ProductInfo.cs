using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProductInfo : UI_Popup
{
    public Image ProductImage;
    public Text Price;
    public Button BuyButton;

    public Product product;

    public void Initialize(Product product)
    {
        ProductImage.sprite = product.Sprite;
        Price.text = $"{product.Price}";
        BuyButton.onClick.AddListener(() =>
        {
            product.Action.Invoke(product);
        });
    }
}
