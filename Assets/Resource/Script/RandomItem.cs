using Unity.VisualScripting;
using UnityEngine;

public class RandomItem : Item
{
    public override void UseItem()
    {
        int rnd = Random.Range(0, 4);
        
        Item item = null;
        switch (rnd)
        {
            case 0:
                item = gameObject.AddComponent<Speed>();
                break;
            case 1:
                item = gameObject.AddComponent<Protection>();
                break;
            case 2:
                item = gameObject.AddComponent<Heal>();
                break;
            case 3:
                item = gameObject.AddComponent<Speed>();
                break;
            default:
                break;
        }
        item.controller = this.controller;
        item.UseItem();
        transform.position = new Vector2(1000, 0);
    }
}
