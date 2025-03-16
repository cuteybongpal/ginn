using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    protected void Init()
    {
        UIManager.Instance.ShowPopup(this);
    }
    public void Hide()
    {
        UIManager.Instance.HidePopUp();
    }
}
