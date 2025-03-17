using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerChasing : MonoBehaviour
{
    Vector3 CameraPos = Vector3.zero;
    public GameObject Player;
    float dis;
    void Start()
    {
        CameraPos = transform.position - Player.transform.position;
        dis = CameraPos.magnitude;
    }
    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(Player.transform.position, CameraPos, dis);
        
        Vector3 collisionPos = Vector3.zero;

        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Attack"))
                continue;
            collisionPos = hit.point;
            break;
        }
        if (collisionPos != Vector3.zero)
        {
            transform.position = collisionPos - CameraPos.normalized * .1f;
        }
        else
        {
            transform.position = Player.transform.position + CameraPos;
        }
    }
}
