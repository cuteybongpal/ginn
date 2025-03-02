using System.Collections;
using UnityEngine;

public class Invincible : Item
{
    public override void UseItem()
    {
        StartCoroutine(Inv());
        StartCoroutine(SetImage2ItemSprite());
        transform.position = new Vector2(1000, 0);
    }
    IEnumerator Inv()
    {
        controller.IsInvincible = true;
        yield return new WaitForSeconds(3f);
        controller.IsInvincible = false;
    }
    IEnumerator SetImage2ItemSprite()
    {
        UI_PlayerUI.UseItem.Invoke(Sprite);
        yield return new WaitForSeconds(3f);
        UI_PlayerUI.UseItem.Invoke(null);
    }

}
