using System.Collections;
using UnityEngine;

public class Heal : Item
{
    public override void UseItem()
    {
        controller.Heal();
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
