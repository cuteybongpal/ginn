using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    public override void UseItem()
    {
        StartCoroutine(DamageBuff());
    }
    IEnumerator DamageBuff()
    {
        GameManager.Instance.PlayerDamage += 1;
        yield return new WaitForSeconds(5f);
        GameManager.Instance.PlayerDamage -= 1;
    }
    public override object Clone()
    {
        GameObject go = Instantiate(gameObject);
        go.transform.position = Vector3.up * 100;
        return go.GetComponent<DamageUp>();
    }
}
