using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected Animator animator;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Activate()
    {

    }
    public virtual void DeActivate()
    {

    }
}
