using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public UI_Base CurrentMainUI;

    public Stack<UI_Popup> Popup = new Stack<UI_Popup>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
    }
    public void ShowPopup(UI_Popup popup)
    {

        Popup.Push(popup);
        Debug.Log("Çª½¬ : "+popup);
    }
    public void HidePopUp()
    {
        if (Popup.Count == 0)
        {
            Debug.Log("ÆË¾÷ÀÌ ¾øÀ½");
            return;
        }
        Debug.Log("»¤ : "+Popup.Peek());
        UI_Popup popup = Popup.Pop();
        if (popup == null)
            return;
        Destroy(popup.gameObject);
    }
}
