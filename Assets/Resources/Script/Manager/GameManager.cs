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
    
    public List<Treasure> Inventory = new List<Treasure>();

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

    public bool Add(Treasure treasure)
    {
        int weight = 0;
        Debug.Log(Inventory.Count);
        if (Inventory.Count >= MaxContainTreauseCount)
        {
            UI_warning ui = Instantiate(UI_Warning).GetComponent<UI_warning>();
            ui.Initialize("���� �κ��丮�� �� á���ϴ�.");
            return false;
        }

        foreach (Treasure t in Inventory)
        {
            weight += t.Weight;
            Debug.Log(weight);
        }
        if (weight > MaxStorableWeight)
        {
            UI_warning ui = Instantiate(UI_Warning).GetComponent<UI_warning>();
            ui.Initialize("���� �������δ� ���Ը� ������ �� �����ϴ�.");
            return false;
        }

        Inventory.Add(treasure);
        return true;
    }

    public void Escape()
    {
        
    }
    public void GameOver()
    {
        DataManager.Instance.stageTreasures[CurrentStage - 1] = TreasureList;
        Debug.Log("���ӿ���");
    }
}
