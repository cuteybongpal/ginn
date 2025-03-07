using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster3 : MonsterController
{
    
    void Update()
    {
        
    }
    protected override void SetPos(Vector2Int pos)
    {
        base.SetPos(pos);
        if (StageManager.Instance.Map.GetTileByPos(pos).TileTypes == Tile.TileType.Cured)
            Flash();
    }
    void Flash()
    {
        int rndX = 0;
        int rndY = 0;
        while (true)
        {
            rndX = Random.Range(-16, 16);
            rndY = Random.Range(-16, 16);

            Tile tile = StageManager.Instance.Map.GetTileByPos(new Vector2Int(rndX, rndY));

            if (tile.TileTypes == Tile.TileType.Blocked || tile.TileTypes == Tile.TileType.PreCured)
                continue;

            break;
        }
        SetPos(new Vector2Int(rndX, rndY));
    }
}
