using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = new DataManager();
    
    public static DataManager Instance {  get { return instance; } }

    public List<Treasure> treasures = new List<Treasure>();
    XmlSerializer serializer = new XmlSerializer(typeof(GameData)); 


    public List<TreasureData> treasureDatas = new List<TreasureData>();

    public List<Treasure>[] stageTreasures = new List<Treasure>[5];
    public void Save(GameData gameData)
    {
        GameData data = gameData;
        using (StreamWriter sw = new StreamWriter("Assets\\Resources\\Data\\data.xml"))
        {
            serializer.Serialize(sw, data);
        }
    }

    public List<Product> Products = new List<Product>();
    public GameData Load()
    {
        GameData data = null;
        using (StreamReader sw = new StreamReader("Assets\\Resources\\Data\\data.xml"))
        {
            data = (GameData)serializer.Deserialize(sw);
        }
        return data;
    }
    public void GetTreasures()
    {
        XmlSerializer treasureSerializer = new XmlSerializer(typeof(List<TreasureData>));
        using (StreamReader sr = new StreamReader("Assets\\Resources\\Data\\treasures.xml"))
        {
            treasureDatas = (List<TreasureData>)treasureSerializer.Deserialize(sr);
        }

        for (int i = 0; i < treasureDatas.Count; i++)
        {
            Treasure treasure = new Treasure();
            treasure.Name = treasureDatas[i].Name;
            treasure.IsMainTreasure = treasureDatas[i].IsMainTreasure;
            treasure.Worth = treasureDatas[i].Worth;
            treasure.Weight = treasureDatas[i].Weight;
            treasure.Sprite = ResourceManager.GetTreasureSprite(treasureDatas[i].SpriteName);
            treasures.Add(treasure);
        }

        stageTreasures[0] = new List<Treasure>()
        {
            new Treasure(treasures[0]),
            new Treasure(treasures[5]),
            new Treasure(treasures[6]),
            new Treasure(treasures[7])
        };

        stageTreasures[1] = new List<Treasure>()
        {
            new Treasure(treasures[1]),
            new Treasure(treasures[8]),
            new Treasure(treasures[9]),
            new Treasure(treasures[10])
        };

        stageTreasures[2] = new List<Treasure>()
        {
            new Treasure(treasures[2]),
            new Treasure(treasures[5]),
            new Treasure(treasures[6]),
            new Treasure(treasures[7])
        };

        stageTreasures[3] = new List<Treasure>()
        {
            new Treasure(treasures[3]),
            new Treasure(treasures[8]),
            new Treasure(treasures[9]),
            new Treasure(treasures[10])
        };

        stageTreasures[4] = new List<Treasure>()
        {
            new Treasure(treasures[4]),
            new Treasure(treasures[5]),
            new Treasure(treasures[6]),
            new Treasure(treasures[7])
        };

    }
    public void Init()
    {
        GetTreasures();

        Products.Add(new Product("가방(중)", (product) =>
        {
            if (product.IsBuy)
                return false;
            if (GameManager.Instance.MaxStorableWeight > 15)
                return false;

            if (GameManager.Instance.CurrentMoney < product.Price)
                return false;
            GameManager.Instance.CurrentMoney -= product.Price;
            GameManager.Instance.MaxStorableWeight = 15;
            GameManager.Instance.MaxContainTreauseCount = 6;
            product.IsBuy = true;
            return true;
        }, 1500, ResourceManager.GetProductSprite("가방_중")));

        Products.Add(new Product("가방(대)", (product) =>
        {
            if (product.IsBuy)
                return false;
            if (GameManager.Instance.MaxStorableWeight > 20)
                return false;

            if (GameManager.Instance.CurrentMoney < product.Price)
                return false;

            GameManager.Instance.CurrentMoney -= product.Price;
            GameManager.Instance.MaxStorableWeight = 20;
            GameManager.Instance.MaxContainTreauseCount = 8;
            product.IsBuy = true;

            return true;
        }, 2000, ResourceManager.GetProductSprite("가방_대")));

        Products.Add(new Product("산소통(소)", (product) =>
        {
            if (product.IsBuy)
                return false;
            if (GameManager.Instance.PlayerMaxOxygenGage > 150)
                return false;

            if (GameManager.Instance.CurrentMoney < product.Price)
                return false;

            GameManager.Instance.CurrentMoney -= product.Price;
            GameManager.Instance.PlayerMaxOxygenGage = 150;
            product.IsBuy = true;
            return true;
        }, 500, ResourceManager.GetProductSprite("산소통_소")));

        Products.Add(new Product("산소통(중)", (product) =>
        {
            if (product.IsBuy)
                return false;
            if (GameManager.Instance.PlayerMaxOxygenGage > 200)
                return false;

            if (GameManager.Instance.CurrentMoney < product.Price)
                return false;

            GameManager.Instance.CurrentMoney -= product.Price;
            GameManager.Instance.PlayerMaxOxygenGage = 200;
            product.IsBuy = true;
            return true;
        }, 1000, ResourceManager.GetProductSprite("산소통_중")));

        Products.Add(new Product("산소통(대)", (product) =>
        {
            if (product.IsBuy)
                return false;
            if (GameManager.Instance.PlayerMaxOxygenGage > 250)
                return false;
            if (GameManager.Instance.CurrentMoney < product.Price)
                return false;

            GameManager.Instance.CurrentMoney -= product.Price;
            GameManager.Instance.PlayerMaxOxygenGage = 250;
            product.IsBuy = true;
            return true;
        }, 1500, ResourceManager.GetProductSprite("산소통_대")));
    }
}
public class GameData
{
    public bool[] IsStageClear = new bool[5];
    public int CurrentMoney = 0;
    public GameData()
    {

    }
}


public class TreasureData
{
    public string SpriteName;
    public int Worth;
    public int Weight;
    public string Name;
    public bool IsMainTreasure;


    public TreasureData(string spriteName, int worth, int weight, string name, bool isMainTreasure)
    {
        this.Name = name;
        this.SpriteName = spriteName;
        this.Worth = worth;
        this.Weight = weight;
        this.IsMainTreasure = isMainTreasure;
    }
    public TreasureData() { }
}

public class Product
{
    public string Name;
    public Func<Product, bool> Action;
    public int Price;

    public bool IsBuy;
    public Sprite Sprite;

    public Product()
    {

    }
    public Product(string name, Func<Product, bool> action, int price, Sprite sprite)
    {
        this.Name = name;
        this.Action = action;
        this.Price = price;
        this.Sprite = sprite;
    }
}