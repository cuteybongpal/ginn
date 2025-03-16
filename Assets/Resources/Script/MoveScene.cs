using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public GameObject MoveToNextSceneUIPrefab;
    public string text;
    public int SceneNum;
    UI_MoveToNextScene ui;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (ui != null)
            return;

        ui = Instantiate(MoveToNextSceneUIPrefab).GetComponent<UI_MoveToNextScene>();

        ui.Initialize(text, SceneNum);
    }
}
