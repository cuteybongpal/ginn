using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPill : Item
{
    public override void UseItem()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.CurrentOxygenGage += 20;
        transform.position = Vector3.up * 100;
    }
    
}
