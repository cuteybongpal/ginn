using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public PlayerController Controller;
    public float Distance;
    Rigidbody rb;
    public float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector2 Pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 PlayerPos = new Vector2(Controller.transform.position.x, Controller.transform.position.z);
        if (Vector2.Distance(Pos, PlayerPos) <= Distance && !Controller.isInvisible)
        {
            Vector2 dir = (PlayerPos - Pos).normalized;
            rb.velocity = new Vector3(dir.x, 0, dir.y) * Speed;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
