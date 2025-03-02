using UnityEngine;

public class Cheat : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StageManager.Instance.StageClear();
            GameManager.Instance.GameClear();
        }
    }
}
