using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void UseItem()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        UseItem();
    }
}
