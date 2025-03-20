using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public GameObject UI_Puzzle;
    GameObject ui;
    bool isClear;
    public static Action PuzzleClear;
    private void Start()
    {
        isClear = DataManager.Instance.isPuzzleCleard[GameManager.Instance.CurrentStage];
        PuzzleClear = Open;
        if (isClear)
            Open();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (ui != null)
            return;
        if (isClear)
            return;
        ui = Instantiate(UI_Puzzle);
    }
    public void Open()
    {
        Debug.Log("¼º°ø");
        DataManager.Instance.isPuzzleCleard[GameManager.Instance.CurrentStage] = true;
        isClear = true;
        StartCoroutine(CageOpen());
    }
    IEnumerator CageOpen()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        Vector3 OriginPos = transform.position;
        Vector3 Targetpos = OriginPos + Vector3.down * 5;
        Debug.Log(gameObject.name);
        while (duration > elapsedTime)
        {
            Vector3 pos = Vector3.Lerp(OriginPos, Targetpos, elapsedTime / duration);
            transform.position = pos;
            Debug.Log($"{transform.position}, {pos}");
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
    }
}
