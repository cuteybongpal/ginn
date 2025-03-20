using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Item
{
    public Material[] materials;
    PlayerController player;
    public override void UseItem()
    {
        player = FindAnyObjectByType<PlayerController>();
        StartCoroutine(PlayerInvisible());
        transform.position = Vector3.up * 100;
    }

    IEnumerator PlayerInvisible()
    {
        player.isInvisible = true;
        Color[] colors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            colors[i] = materials[i].color;
            materials[i].color = new Color(colors[i].r, colors[i].g, colors[i].b, 0.5f);
        }
        yield return new WaitForSeconds(3);
        player.isInvisible = false;
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = new Color(colors[i].r, colors[i].g, colors[i].b, 1f);
        }
    }
    public override object Clone()
    {
        GameObject go = Instantiate(gameObject);
        go.transform.position = Vector3.up * 100;
        return go.GetComponent<Invisible>();
    }
}
