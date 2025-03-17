using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Item : MonoBehaviour, StorableItem
{
    public Sprite ItemSprite;
    public void Store()
    {
        GameManager.Instance.Add(this);
        UI_PlayerUI ui = UIManager.Instance.CurrentMainUI as UI_PlayerUI;
        ui.SetInventoryImage(ItemSprite);

    }

    public virtual void UseItem()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Store();
        transform.position = Vector3.up * 100;
    }
}
