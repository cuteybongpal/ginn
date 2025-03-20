using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Heal : Item
{
    public override void UseItem()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.CurrentHp++;
        transform.position = Vector3.up * 100;
    }
    public override object Clone()
    {
        GameObject go = Instantiate(gameObject);
        go.transform.position = Vector3.up * 100;
        return go.GetComponent<Heal>();
    }
}
