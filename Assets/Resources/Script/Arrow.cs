using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public float Speed;
    Rigidbody rb;

    public void Init(Vector3 dir)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir * Speed;
        Vector3 angles = Quaternion.LookRotation(dir).eulerAngles + new Vector3(90, 0, 0);
        transform.rotation = Quaternion.Euler(angles);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Monster"))
            return;

        Debug.Log("적들에게 닿음!");
        Destroy(gameObject);
    }
}
