using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidance : Item
{
    public GameObject Arrow;
    PlayerController player;
    public override void UseItem()
    {
        player = FindAnyObjectByType<PlayerController>();
        StartCoroutine(arrow());
        transform.position = Vector3.up * 100;
    }
    IEnumerator arrow()
    {
        float duration = 10f;
        float elapsedTime = 0f;
        Vector3 TreasurePos = FindAnyObjectByType<TreasueChest>().transform.position;
        GameObject go = Instantiate(Arrow);
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
                break;
            Vector3 PlayerPos = player.transform.position;
            Vector2 dir = new Vector2(TreasurePos.x - PlayerPos.x, TreasurePos.z - PlayerPos.z).normalized;
            go.transform.position = new Vector3(dir.x, .5f, dir.y) + player.transform.position;
            go.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
            go.transform.Rotate(new Vector3(90,0,0));
            yield return null;
        }
        Destroy(go);
    }
}
