using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform[] TargetPoses;
    int TargetposIndex = 1;
    Vector3 Origin;
    public float Speed;
    float duration = 0;
    private void Start()
    {
        Origin = TargetPoses[0].position;
        
        CommandMove();
    }
    void CommandMove()
    {
        TargetposIndex = (TargetposIndex + 1) % TargetPoses.Length;
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        duration = 1 / Speed;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(Origin, TargetPoses[TargetposIndex].position, elapsedTime / duration);
            Vector2 dir = new Vector2(pos.x - transform.position.x, pos.z - transform.position.z);
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
            transform.position = pos;
            yield return null;
        }
        Origin = TargetPoses[TargetposIndex].position;
        CommandMove();
    }
}
