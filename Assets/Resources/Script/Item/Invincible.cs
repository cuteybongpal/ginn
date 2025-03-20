using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : Item
{
    PlayerController player;
    public override void UseItem()
    {
        player = FindAnyObjectByType<PlayerController>();
        StartCoroutine(Invin());
    }
    IEnumerator Invin()
    {
        player.isPlayerInvincible = true;
        yield return new WaitForSeconds(2);
        player.isPlayerInvincible = false;
    }
    public override object Clone()
    {
        GameObject go = Instantiate(gameObject);
        go.transform.position = Vector3.up * 100;
        return go.GetComponent<Invincible>();
    }
}
