using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public PlayerController Controller;
    public float Distance;
    Rigidbody rb;
    public float Speed;
    bool isKnockBacking;
    public int MaxHp;
    public int CurrentHp;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CurrentHp = MaxHp;
    }
    void Update()
    {
        Vector2 Pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 PlayerPos = new Vector2(Controller.transform.position.x, Controller.transform.position.z);
        if (Vector2.Distance(Pos, PlayerPos) <= Distance && !Controller.isInvisible && !isKnockBacking)
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
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Attack"))
            return;
        Damaged();
        Vector3 dir = other.gameObject.transform.position - gameObject.transform.position;
        StartCoroutine(Knockback(dir));
    }
    IEnumerator Knockback(Vector3 dir)
    {
        isKnockBacking = true;
        rb.velocity = new Vector3(dir.x,0,dir.y).normalized * 10;
        yield return new WaitForSeconds(.3f);
        rb.velocity = Vector3.zero;
        isKnockBacking = false;
    }
    void Damaged()
    {
        Debug.Log("데미지 입음");
        CurrentHp -= GameManager.Instance.PlayerDamage;
        if (CurrentHp <= 0)
            Destroy(gameObject);
    }
}
