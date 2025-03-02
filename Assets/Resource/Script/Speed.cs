using System.Collections;
using UnityEngine;

public class Speed : Item
{
    public override void UseItem()
    {
        StartCoroutine(Boost());
        StartCoroutine(SetImage2ItemSprite());
        transform.position = new Vector2(1000, 0);
    }
    IEnumerator Boost()
    {
        controller.Speed += 2;
        yield return new WaitForSeconds(2);
        controller.Speed -= 2;
        
    }

    IEnumerator SetImage2ItemSprite()
    {
        UI_PlayerUI.UseItem.Invoke(Sprite);
        yield return new WaitForSeconds(2f);
        UI_PlayerUI.UseItem.Invoke(null);
    }
}