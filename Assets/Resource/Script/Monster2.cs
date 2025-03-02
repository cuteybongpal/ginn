using UnityEngine;

public class Monster2 : MonsterController
{
    //몬스터의 몸이 차지하는 좌표
    // 0,0에 있으면 1,1에서 -1,-1까지가 몬스터의 크기임
    Vector2Int[] size = new Vector2Int[9]
    {
        new Vector2Int(1,1),
        new Vector2Int(0,1),
        new Vector2Int(-1,1),
        new Vector2Int(1,0),
        new Vector2Int(0,0),
        new Vector2Int(-1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1)
    };
    protected override void Start()
    {
        base.Start();
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
                    Player.Damaged();
                }
            }
            else
                tile.SetTileType(Map.TileType.Corrupted);
        }
    }
}