using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int MaxHp;
    int currentHp;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {

            currentHp = value;
            if (currentHp > MaxHp) 
                currentHp = MaxHp;
            if (isLobby)
                return;
            UI_PlayerUI ui = UIManager.Instance.CurrentMainUI as UI_PlayerUI;

            ui.SetHPBar(currentHp);
            if (currentHp <= 0)
                GameManager.Instance.GameOver();
        }
    }
    public float Speed;

    int currentOxygenGage;

    public bool isLobby;
    public bool isInvisible;
    public int CurrentOxygenGage {  
        get { return currentOxygenGage; }
        set
        {
            currentOxygenGage = value;
            UI_PlayerUI ui = UIManager.Instance.CurrentMainUI as UI_PlayerUI;
            if (ui == null)
                return;

            if (currentOxygenGage > GameManager.Instance.PlayerMaxOxygenGage)
                currentOxygenGage = GameManager.Instance.PlayerMaxOxygenGage;
            ui.SetOxygenGageValue(value);
        }
    }

    Rigidbody rb;

    public GameObject Arrow;
    bool isCanJump = true;
    bool isCanAttack = true;


    public float AttackCoolDown;
    public enum PlayerState
    {
        Run,
        Jump,
        Idle,
        Attack
    }

    PlayerState state = PlayerState.Idle;
    Animator anim;

    public PlayerState State
    {
        get { return state; }
        set
        {
            state = value;
            switch (state)
            {
                case PlayerState.Run:
                    Run();
                    break;
                case PlayerState.Jump:
                    Jump();
                    break;
                case PlayerState.Idle:
                    Idle();
                    break;
                case PlayerState.Attack:
                    Attack();
                    break;
            }

        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        CurrentOxygenGage = GameManager.Instance.PlayerMaxOxygenGage;
        CurrentHp = MaxHp;
        if (!isLobby)
            StartCoroutine(ReduceCurrentOxygen());
    }

    IEnumerator ReduceCurrentOxygen()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            CurrentOxygenGage--;
        }
    }
    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCanJump)
            {
                State = PlayerState.Jump;
                rb.velocity += Vector3.up * 5;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            State = PlayerState.Attack;
        }
        if (dir != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(dir.normalized);
            //float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            State = PlayerState.Run;
            dir = dir.normalized;
        }
        else
        {
            dir = dir.normalized;
            State = PlayerState.Idle;
        }
        rb.velocity = dir * Speed + new Vector3(0, rb.velocity.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, ray.direction, 100);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);
        Vector2 hitPos = Vector2.zero;
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.CompareTag("Ground"))
                continue;
            hitPos = new Vector2(hit.point.z - transform.position.z, hit.point.x - transform.position.x);
            break;
        }
        float angle = Mathf.Atan2(hitPos.y, hitPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
    void Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    void Run()
    {
        anim.SetFloat("Speed", 1);
    }
    void Attack()
    {
        if (isLobby)
            return;
        if (!isCanAttack)
            return;
        anim.Play("BowShot");
        StartCoroutine(CoolDown());
    }
    IEnumerator CoolDown()
    {
        isCanAttack = false;
        yield return new WaitForSeconds(AttackCoolDown);
        isCanAttack = true;
    }
    public void Damaged()
    {
        if (CurrentHp <= 0)
            return;
        CurrentHp -= 1;
    }

    void Jump()
    {
        anim.Play("Jump");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trap") && !other.CompareTag("Monster"))
            return;

        Damaged();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
            isCanJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
            isCanJump = false;
    }
    void ShotArrow()
    {
        GameObject go = Instantiate(Arrow);
        Arrow arrow = go.GetComponent<Arrow>();
        float angleZ = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        float angleX = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        Vector3 dir = new Vector3(angleX, 0, angleZ);

        go.transform.position = transform.position + Vector3.up * 1.5f + dir * .7f;



        
        arrow.Init(dir);
    }
}
