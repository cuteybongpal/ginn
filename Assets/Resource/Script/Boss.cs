using System.Collections;
using UnityEngine;

public class Boss : MonsterController
{
    Vector2Int[] size = new Vector2Int[]
    {
        new Vector2Int(-1, 2),
        new Vector2Int(0, 2),
        new Vector2Int(1, 2),
        new Vector2Int(2, 1),
        new Vector2Int(1, 1),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 1),
        new Vector2Int(-2, 1),
        new Vector2Int(2,0),
        new Vector2Int(1,0),
        new Vector2Int(0,0),
        new Vector2Int(-1,0),
        new Vector2Int(-2,0),
        new Vector2Int(2,-1),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-2, -1),
        new Vector2Int(-1,-2),
        new Vector2Int(0,-2),
        new Vector2Int(1,-2),
    };
    bool isDamaged = false;
    void Start()
    {
        base.Start();
        Player = StageManager.Instance.Player;
    }

    protected override bool IsAvaliableDir(Vector2Int dir)
    {

        bool isNotBlocked = true;
        for (int i = 0; i < size.Length; i++)
        {
            Vector2Int pos = CurrentPos;
            pos = pos + dir + size[i];
            Tile tile = StageManager.Instance.GetMap().GetTileByPos(pos);
            if (tile == null)
            {
                isNotBlocked = false;
            }
            else if (tile.Type == Map.TileType.Blocked)
            {
                isNotBlocked = false;
            }
            else
            {
                isNotBlocked &= true;
            }
        }
        return isNotBlocked;
    }
    protected override void SetPos()
    {
        for (int i = 0; i < size.Length; i++)
        {
            Vector2Int pos = CurrentPos;
            pos = pos + size[i];
            Tile tile = StageManager.Instance.GetMap().GetTileByPos(pos);

            tile.monster = null;
        }

        CurrentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        int damageCount = 0;
        for (int i = 0; i < size.Length; i++)
        {
            Vector2Int pos = CurrentPos;
            pos = pos + size[i];
            Tile tile = StageManager.Instance.GetMap().GetTileByPos(pos);
            tile.monster = this;
            if (tile.Type == Map.TileType.PreCured)
            {
                if (damageCount == 0)
                {
                    damageCount++;
                    //Player.Damaged();
                }
            }
            else
                tile.SetTileType(Map.TileType.Corrupted);
        }
    }


    public override void Damaged(int damage)
    {
        if (isDamaged)
            return;
        if (CurrentHp <= 0)
            return;
        CurrentHp -= 1;
        isDamaged = true;
        StartCoroutine(WaitOneFrameAndChangeIsDamaged());
        if (CurrentHp <= 0)
            Die();

    }
    IEnumerator WaitOneFrameAndChangeIsDamaged()
    {
        yield return null;
        isDamaged = false;
    }
    public IEnumerator ReSpawnMonsters()
    {
        int monsterCount = StageManager.Instance.MonsterCount;
        for (int i = 0; i < monsterCount; i++)
        {
            MonsterController monster = Instantiate(StageManager.Instance.Monster1Prefab).GetComponent<MonsterController>();
            monster.transform.position = transform.position;
            monster.Player = StageManager.Instance.Player;
            StageManager.Instance.monsters.Add(monster);
            yield return new WaitForSeconds(0.2f);
        }

    }
    public override void Die()
    {
        StageManager.Instance.Boss = null;
        Destroy(gameObject);
    }
}
