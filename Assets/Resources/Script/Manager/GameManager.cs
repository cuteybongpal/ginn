using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    
    public List<StorableItem> Inventory = new List<StorableItem>();

    List<StorableItem> prevInventory = new List<StorableItem>();

    public GameObject UI_Warning;
    public GameObject UI_GameOver;

    int currentMoney = 0;
    public int CurrentMoney 
    {
        get { return currentMoney; }
        set
        {
            currentMoney = value;
            UI_Lobby ui = UIManager.Instance.CurrentMainUI as UI_Lobby;
            if (ui == null)
                return;
            ui.SetMoneyText(currentMoney);
        }
    }
    public int MaxStorableWeight = 10;
    public int MaxContainTreauseCount = 4;

    public int PlayerMaxOxygenGage = 100;
    private int currentStage = 0;
    public int CurrentStage
    {
        get { return currentStage; }
        set
        {
            currentStage = value;
            if (value != 0)
                GameStart();
            SceneManager.LoadSceneAsync(value, LoadSceneMode.Single);
        }
    }
    public int PlayerDamage = 1;

    public int PlayerCurrentHP = 0;
    public int PlayerCurrentOxygenGage = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public List<Treasure> TreasureList = new List<Treasure>();
    public void GameStart()
    {
        Time.timeScale = 1;
        TreasureList = new List<Treasure>();
        prevInventory = new List<StorableItem>();
        foreach (Treasure t in DataManager.Instance.stageTreasures[CurrentStage - 1])
        {
            Treasure tt;
            if (t == null)
                tt = null;
            else
                tt = (Treasure)t.Clone();
            TreasureList.Add(tt);
        }
        for (int i = 0; i < Inventory.Count; i++)
        {
            Treasure t;
            Item it;
            if (Inventory[i] is Treasure)
            {
                t = (Treasure)Inventory[i].Clone();
                prevInventory.Add(t);
            }
            else
            {
                it = (Item)Inventory[i].Clone();
                DontDestroyOnLoad(it);
                prevInventory.Add(it);
            }            
        }
    }
    public bool Add(StorableItem storable)
    {
        int weight = 0;
        if (Inventory.Count >= MaxContainTreauseCount)
        {
            UI_warning ui = Instantiate(UI_Warning).GetComponent<UI_warning>();
            ui.Initialize("현재 인벤토리가 꽉 찼습니다.");
            return false;
        }

        foreach (StorableItem t in Inventory)
        {
            Treasure treasure = t as Treasure;
            if (treasure == null)
                continue;
            weight += treasure.Weight;
        }
        if (storable is Treasure)
        {
            Treasure t = storable as Treasure;
            weight += t.Weight;
        }
        if (weight > MaxStorableWeight)
        {
            UI_warning ui = Instantiate(UI_Warning).GetComponent<UI_warning>();
            ui.Initialize("너무 무거워요 ㅠㅠ");
            PlayerController player = FindAnyObjectByType<PlayerController>();
            player.Speed -= 2;
        }
        
        if (storable is Item)
        {
            Item origin = (Item)storable;
            Item item = storable.Clone() as Item;
            DontDestroyOnLoad(item);
            Destroy(origin.gameObject);
            Inventory.Add(item);
        }
        else
        {
            Inventory.Add(storable);
        }
        return true;
    }
    public void Remove(int index)
    {
        UI_PlayerUI ui = UIManager.Instance.CurrentMainUI as UI_PlayerUI;
        for (int i = 0; i < Inventory.Count; i++)
        {
            ui.UseItem(i);
        }
        Inventory.RemoveAt(index);
        for (int i = 0; i < Inventory.Count; i++)
        {
            Treasure t = Inventory[i] as Treasure;
            Item item = Inventory[i] as Item;
            if (t != null)
                ui.SetInventoryImage(t.Sprite);
            else
                ui.SetInventoryImage(item.ItemSprite);
        }
    }
    public void GameOver()
    {
        List<Treasure> tList = new List<Treasure>();
        foreach (Treasure t in TreasureList)
        {
            if (t == null)
                tList.Add(null);
            else
                tList.Add(t.Clone() as Treasure);
        }
        DataManager.Instance.stageTreasures[CurrentStage - 1] = tList;
        TreasureList.Clear();
        Inventory = prevInventory;
        Instantiate(UI_GameOver);
        Time.timeScale = 0;
    }
}
