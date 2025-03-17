using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    public float CoolDown;
    Collider AttackCollider;
    void Start()
    {
        AttackCollider = GetComponent<Collider>();
        AttackCollider.enabled = false;
        StartCoroutine(SpearCycle());
    }
    IEnumerator SpearCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoolDown);

            yield return SpearUp();
            AttackCollider.enabled = true;

            yield return SpearDown();
            AttackCollider.enabled = false;
        }
    }
    IEnumerator SpearUp()
    {
        Vector3 OriginPos = transform.position;
        float duration = .3f;
        float elapsedTime = 0f;
        while (duration > elapsedTime)
        {
            Vector3 pos = Vector3.Lerp(OriginPos, OriginPos + Vector3.up * 2, elapsedTime / duration);
            transform.position = pos;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
    IEnumerator SpearDown()
    {
        Vector3 OriginPos = transform.position;
        float duration = .3f;
        float elapsedTime = 0f;
        while (duration > elapsedTime)
        {
            Vector3 pos = Vector3.Lerp(OriginPos, OriginPos + Vector3.down * 2, elapsedTime / duration);
            transform.position = pos;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
