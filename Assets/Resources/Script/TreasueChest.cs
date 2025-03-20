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
    public AudioClip[] Clips;
    /// <summary>
    /// 0 : ¿­±â
    /// 1 : ´Ý±â
    /// </summary>
    AudioSource[] sources;
    void Start()
    {
        anim = GetComponent<Animator>();
        treasure = GameManager.Instance.TreasureList[ChestNumber];
        sources = GetComponents<AudioSource>();
        sources[0].clip = Clips[0];
        sources[1].clip = Clips[1];
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
        sources[0].Play();
    }
    public void Close()
    {

        anim.SetBool("open", false);
        anim.SetBool("close", true);
        sources[1].Play();
    }

    public void Take()
    {
        UI_PlayerUI ui = UIManager.Instance.CurrentMainUI as UI_PlayerUI;
        ui.SetInventoryImage(treasure.Sprite);
        treasure = null;
        DataManager.Instance.stageTreasures[(GameManager.Instance.CurrentStage - 1)][ChestNumber] = null;
    }
}
