using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    Text text;
    void Awake()
    {
        text = GetComponent<Text>();
    }
    public void Set(int value)
    {
        text.text = $"Score : {value}";
    }
}
