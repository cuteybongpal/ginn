using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int MaxHp;
    int currentHp;
    public SpriteRenderer spriteRenderer;
    float speed = 10;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            if (value > 10)
            {
                animator.Play("SpeedEffect");
            }
            else
            {
                animator.Play("None");
            }
        }
    }
    public Animator animator;
    bool isProtection;
    bool isinvincible;
    public bool IsProtection
    {
        get { return isProtection; }
        set
        {
            isProtection = value;
            if (value)
                animator.Play("ProtectionEffect");
            else
                animator.Play("None");
        }
    }
    public bool IsInvincible { 
        get { return isinvincible; }
        set
        {
            isinvincible = value;
            if (value)
                animator.Play("InvincibleEffect");
            else
                animator.Play("None");

        }
    }

    public int CurrentHp { 
        get
        {
            return currentHp; 
        }
        set
        {
            currentHp = value;
            Color color = new Color(0, 1, 0);
            switch (currentHp)
            {
                case 5:
                    color = new Color(0, 1, 0);
                    break;
                case 4:
                    color = new Color(0.5f, 1, 0);
                    break;
                case 3:
                    color = new Color(1f, 1f, 0);
                    break;
                case 2:
                    color = new Color(1f, 0.5f, 0);
                    break;
                case 1:
                    color = new Color(1, 0, 0);
                    break;
                case 0:
                    Die();
                    break;
            }
            if (spriteRenderer == null)
                spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }
    }
    public Vector2Int CurrentPos;
    Vector2Int currentDir;


    void Start()
    {
        StageManager.Instance.Player = this;
        animator = transform.GetChild(1).GetComponent<Animator>();
        animator.enabled = true;
        CurrentPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        CurrentHp = MaxHp;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (currentDir == -Vector2Int.up)
                return;
            
            if (StageManager.Instance.MovePlayer(Vector2Int.up, Speed))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                currentDir = Vector2Int.up;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (currentDir == -Vector2Int.down)
                return;
            
            if (StageManager.Instance.MovePlayer(Vector2Int.down, Speed))
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                currentDir = Vector2Int.down;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (currentDir == -Vector2Int.left)
                return;
            
            if (StageManager.Instance.MovePlayer(Vector2Int.left, Speed))
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                currentDir = Vector2Int.left;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (currentDir == -Vector2Int.right)
                return;
            
            if (StageManager.Instance.MovePlayer(Vector2Int.right, Speed))
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                currentDir = Vector2Int.right;
            }
        }
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void Damaged()
    {
        if (IsProtection)
        {
            IsProtection = false;
            return;
        }
        if (IsInvincible)
        {
            return;
        }
        CurrentHp -= 1;
        StageManager.Instance.PlayerHpChanged?.Invoke(CurrentHp);
    }
    public void Heal()
    {
        CurrentHp += 1;
        StageManager.Instance.PlayerHpChanged?.Invoke(CurrentHp);
        animator.Play("HealingEffect");
        StartCoroutine(stopAnimation(2.9f));
    }
    public void Die()
    {
        StageManager.Instance.GameOver();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Damaged();
        }
        else if (collision.CompareTag("Item"))
        {
            collision.GetComponent<Item>().UseItem();
        }
    }
    IEnumerator stopAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.Play("None");
    }
}
