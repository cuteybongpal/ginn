using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerChasing : MonoBehaviour
{
    Vector3 CameraPos = Vector3.zero;
    public GameObject Player;
    void Start()
    {
       CameraPos = transform.position - Player.transform.position;
    }
    void Update()
    {
        transform.position = Player.transform.position + CameraPos;
    }
}
