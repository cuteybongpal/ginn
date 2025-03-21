using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GolemController : MonoBehaviour
{
    public int MaxHp;
    int currentHp;

    public float Speed;

    public PlayerController Player;
    Rigidbody rb;
    Animator anim;
    public float Distance;
    public Collider AttackCollider;
    /// <summary>
    /// 0 : 공격
    /// 1 : 데미지 입음
    /// </summary>
    public ParticleSystem[] Particles;

    public AudioClip[] Audios;
    /// <summary>
    /// 0 : 걷기
    /// 1 : 공격
    /// 2 : 데미지 입음
    /// </summary>
    AudioSource[] AudioSources;
    public enum MonsterState
    {
        Idle,
        Walk,
        Attack
    }
    bool CanAttack = true;
    MonsterState state = MonsterState.Idle;
    MonsterState State 
    {
        get { return state; }
        set
        {
            state = value;
            switch (state)
            {
                case MonsterState.Idle:
                    Idle();
                    break;
                case MonsterState.Walk:
                    Walk();
                    break;
                case MonsterState.Attack:
                    Attack();
                    break;
            }
        }
    }

    public int CurrentHp 
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            if (currentHp > MaxHp)
                currentHp = MaxHp;
            else if (currentHp < 0)
                currentHp = 0;
        }
    }
    private void Start()
    {
        CurrentHp = MaxHp;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        AttackCollider.enabled = false;
        AudioSources = GetComponents<AudioSource>();
        AudioSources[0].clip = Audios[0];
        AudioSources[1].clip = Audios[1];
        AudioSources[2].clip = Audios[2];
    }
    void Update()
    {
        Vector2 Pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 PlayerPos = new Vector2(Player.transform.position.x, Player.transform.position.z);
        if (Vector2.Distance(Pos, PlayerPos) <= Distance && !Player.isInvisible)
        {
            Vector2 dir = (PlayerPos - Pos).normalized;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            if (Vector2.Distance(Pos, PlayerPos) <= 4 && CanAttack)
            {
                rb.velocity = Vector3.zero;
                State = MonsterState.Attack;
            }
            else
            {
                State = MonsterState.Walk;
                rb.velocity = new Vector3(dir.x, 0, dir.y) * Speed;
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
            State = MonsterState.Idle;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (!other.CompareTag("Attack"))
            return;
        Damaged(GameManager.Instance.PlayerDamage);
    }
    void Idle()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return;
        anim.Play("Idle");
    }
    void Walk()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return;
        anim.Play("Walk");
    }
    void Attack()
    {
        anim.Play("Attack");
        
        StartCoroutine(AttackCoolDown());
    }
    IEnumerator AttackCoolDown()
    {
        CanAttack = false;
        yield return new WaitForSeconds(5);
        CanAttack = true;
    }

    void ColliderOn()
    {
        StartCoroutine(Attacking());
        Particles[0].Play();
        AudioSources[1].Play();
    }
    void WalkSound()
    {
        AudioSources[0].Play();
    }
    IEnumerator Attacking()
    {
        AttackCollider.enabled = true;
        yield return new WaitForSeconds(.3f);
        AttackCollider.enabled = false;
    }
    void Damaged(int damage)
    {
        if (CurrentHp <= 0)
            return;
        CurrentHp -= damage;
        Particles[1].Play();
        AudioSources[2].Play();
        if (CurrentHp <= 0)
            Die();

    }
    void Die()
    {
        Destroy(gameObject);
    }
}
