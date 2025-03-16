using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Trap
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Activate()
    {
        base.Activate();
        animator.SetBool("open", true);
        animator.SetBool("close", false);
    }
    public override void DeActivate()
    {
        base.DeActivate();
        animator.SetBool("open", false);
        animator.SetBool("close", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Activate();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        DeActivate();
    }
}
