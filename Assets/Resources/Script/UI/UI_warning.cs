using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_warning : UI_Popup
{
    public Text WarningText;
    public void Initialize(string text)
    {
        Init();
        WarningText.text = text;
        StartCoroutine(ShowAndHidePopup());
    }
    IEnumerator ShowAndHidePopup()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
