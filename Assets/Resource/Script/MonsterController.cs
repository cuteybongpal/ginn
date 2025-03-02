using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float Speed;
    Vector2Int[] dirs = new Vector2Int[4] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    public Vector2Int CurrentPos;

    public int MaxHp;
    public int CurrentHp;
    public PlayerController Player;
    protected bool isMoving;
    public int Score;
    protected virtual void Start()
    {
        CurrentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        transform.position = (Vector2)CurrentPos;
        CurrentHp = MaxHp;
        StartCoroutine(CommandMove());
    }

    IEnumerator CommandMove()
    {
        
        Vector2Int dir = Vector2Int.zero;
        while (true)
        {
            int rnd = Random.Range(0, 4);
            dir = dirs[rnd];
            if (IsAvaliableDir(dir))
                break;
            yield return null;
        }
        StartCoroutine(Move(dir));
    }
    IEnumerator Move(Vector2Int dir)
    {
        isMoving = true;
        float duration = 1 / Speed;
        float elapsedTime = 0;
        Vector2 originPos = transform.position;
        while (duration >= elapsedTime)
        {
            Vector2 pos = Vector2.Lerp(originPos, originPos + dir, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        SetPos();
        isMoving = false;
        StartCoroutine(CommandMove());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public virtual void Damaged(int damage)
    {
        if (CurrentHp <= 0)
            return;
        CurrentHp -= damage;
        if (CurrentHp <= 0)
            Die();
    }
    public virtual void Die()
    {
        StageManager.Instance.Score += Score;
        StageManager.Instance.monsters.Remove(this);
        Destroy(gameObject);
    }
    protected virtual bool IsAvaliableDir(Vector2Int dir)
    {
        Tile tile = StageManager.Instance.GetMap().GetTileByPos(CurrentPos + dir);
        if (tile == null)
            return false;

        if (tile.Type == Map.TileType.Blocked)
            return false;
        
        return true;
    }
    protected virtual void SetPos()
    {
        Tile tile = StageManager.Instance.GetMap().GetTileByPos(CurrentPos);
        tile.monster = null;

        CurrentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        tile = StageManager.Instance.GetMap().GetTileByPos(CurrentPos);
        tile.monster = this;
        
        if (tile.Type == Map.TileType.PreCured)
            Player.Damaged();
        else
            tile.SetTileType(Map.TileType.Corrupted);
    }
}
