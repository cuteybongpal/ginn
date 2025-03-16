using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Item
{
    public float speed;
    PlayerController player;
    public override void UseItem()
    {
        player = GameObject.FindAnyObjectByType<PlayerController>();

        StartCoroutine(boost());

        gameObject.transform.position = Vector3.up * 100;
    }
    IEnumerator boost()
    {
        player.Speed += speed;
        yield return new WaitForSeconds(2);
        player.Speed -= speed;
    }
}
