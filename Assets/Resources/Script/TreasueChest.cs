using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasueChest : MonoBehaviour
{
    public Treasure treasure;
    public GameObject UI_ChestPrefab;
    UI_Chest chestUI;
    Animator anim;

    public int ChestNumber;

    void Start()
    {
        anim = GetComponent<Animator>();
        treasure = DataManager.Instance.stageTreasures[GameManager.Instance.CurrentStage][ChestNumber];
        if ( treasure == null)
        {
            Open();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (treasure == null)
            return;
        if (chestUI != null)
            return;
        chestUI = Instantiate(UI_ChestPrefab).GetComponent<UI_Chest>();
        chestUI.Init(treasure, this);
        Open();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (treasure == null)
            return;
        if (chestUI == null)
            return;

        chestUI.Hide();
        Close();
    }

    public void Open()
    {

        anim.SetBool("open", true);
        anim.SetBool("close", false);
    }
    public void Close()
    {

        anim.SetBool("open", false);
        anim.SetBool("close", true);
    }

    public void Take()
    {
        UI_PlayerUI ui = UIManager.Instance.CurrentMainUI as UI_PlayerUI;
        ui.TakeTreasure(treasure);
        treasure = null;
        DataManager.Instance.stageTreasures[GameManager.Instance.CurrentStage][ChestNumber] = null;
    }
}
