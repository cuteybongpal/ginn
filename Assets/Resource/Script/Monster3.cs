using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class Monster3 : MonsterController
{
    void Start()
    {
        base.Start();
    }

    public override void Damaged(int damage)
    {
        if (CurrentHp <= 0)
            return;
        CurrentHp -= damage;
        if (CurrentHp <= 0)
            Die();
    }
    void Flash()
    {
        Tile tile;
        int count = 0;
        while (true)
        {
            count++;
            
            int rndX = Random.Range(-16, 16);
            int rndY = Random.Range(-16, 16);

            tile = StageManager.Instance.GetMap().GetTileByPos(new Vector2Int(rndX, rndY));

            

            if (tile == null)
                continue;
            if (tile.Type == Map.TileType.Blocked)
                continue;
            if (tile.Type == Map.TileType.Cured)
                continue;
            break;
        }
        transform.position = new Vector2(tile.Pos.x, tile.Pos.y);
        SetPos();
    }
    protected override void SetPos()
    {
        Tile tile = StageManager.Instance.GetMap().GetTileByPos(CurrentPos);
        tile.monster = null;

        CurrentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        tile = StageManager.Instance.GetMap().GetTileByPos(CurrentPos);
        tile.monster = this;

        if (tile.Type == Map.TileType.PreCured)
            Player.Damaged();
        else
        {
            if (tile.Type == Map.TileType.Cured)
                Flash();
            tile.SetTileType(Map.TileType.Corrupted);
        }
            
    }
}
