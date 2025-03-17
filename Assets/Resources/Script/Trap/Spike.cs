using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Trap
{
    public float CoolDown;
    BoxCollider Collider;
    AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        Collider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        DeActivate();
        StartCoroutine(TrapLoop());
    }
    IEnumerator TrapLoop()
    {
        while (true)
        {
            Activate();
            yield return new WaitForSeconds(CoolDown);
            DeActivate();
            yield return new WaitForSeconds(CoolDown);
        }
    }

    public override void Activate()
    {
        animator.SetTrigger("open");
        audioSource.Play();
        Collider.enabled = true;
    }
    public override void DeActivate()
    {
        animator.SetTrigger("close");
        Collider.enabled = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
