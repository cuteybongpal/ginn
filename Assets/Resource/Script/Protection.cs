using System.Collections;
using UnityEngine;

public class Protection : Item
{
    public override void UseItem()
    {
        controller.IsProtection = true;
        transform.position = new Vector2(1000, 0);
        StartCoroutine(SetImage2ItemSprite());
    }
    IEnumerator SetImage2ItemSprite()
    {
        UI_PlayerUI.UseItem.Invoke(Sprite);
        yield return new WaitForSeconds(1f);
        UI_PlayerUI.UseItem.Invoke(null);
    }
}
