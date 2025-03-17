using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    
    public List<StorableItem> Inventory = new List<StorableItem>();

    public GameObject UI_Warning;

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
            Debug.Log(value);
            SceneManager.LoadScene(value);
            if (value != 0)
                GameStart();
        }
    }
    public int PlayerDamage = 1;


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
    public List<Treasure> TreasureList;
    public void GameStart()
    {
        TreasureList = DataManager.Instance.stageTreasures[CurrentStage - 1];
        Debug.Log(Inventory.Count);
    }

    public bool Add(StorableItem storable)
    {
        int weight = 0;
        Debug.Log(Inventory.Count);
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
            player.Speed -= 3;
        }

        Inventory.Add(storable);
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
    public void Escape()
    {
        
    }
    public void GameOver()
    {
        DataManager.Instance.stageTreasures[CurrentStage - 1] = TreasureList;
        Debug.Log("게임오버");
    }
}
