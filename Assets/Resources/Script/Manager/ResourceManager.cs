using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager 
{
    public static List<Sprite> GetTreasureSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprite/Treasure");
        return sprites.ToList();
    }
    public static Sprite GetTreasureSprite(string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprite/Treasure/{spriteName}");
        return sprite;
    }
    public static Sprite GetProductSprite(string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprite/Products/{spriteName}");
        return sprite;
    }
}
