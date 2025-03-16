using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure
{
    public Sprite Sprite;
    public string Name;
    public int Worth;
    public int Weight;
    public bool IsMainTreasure;
    public Treasure() { }


    public Treasure(string name, int price, int weight)
    {
        this.Name = name;
        this.Worth = price;
        this.Weight = weight;
    }
    public Treasure(Treasure origin)
    {
        string name = origin.Name;
        int price = origin.Worth;
        int Weight = origin.Weight;
        bool isMainTreasure = origin.IsMainTreasure;
        Sprite sprite = origin.Sprite;

        this.Name = name;
        this.Worth = origin.Worth;
        this.Weight = origin.Weight;
        this.IsMainTreasure= isMainTreasure;
        this.Sprite = sprite;
    }
}
