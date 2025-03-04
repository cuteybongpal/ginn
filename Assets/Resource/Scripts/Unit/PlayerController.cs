using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2Int currentDir = Vector2Int.zero;

    public Vector2Int CurrentPos = Vector2Int.zero;


    public int MaxHp;
    public int CurrentHp;

    public int Speed;
    void Start()
    {
        transform.position = (Vector2)currentDir;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (currentDir == -Vector2Int.up)
                return;
            currentDir = Vector2Int.up;
            if (StageManager.Instance.PlayerMove(currentDir, CurrentPos))
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (currentDir == -Vector2Int.down)
                return;
            currentDir = Vector2Int.down;
            if(StageManager.Instance.PlayerMove(currentDir, CurrentPos))
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (currentDir == -Vector2Int.right)
                return;
            currentDir = Vector2Int.right;
            if(StageManager.Instance.PlayerMove(currentDir, CurrentPos))
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (currentDir == -Vector2Int.left)
                return;
            currentDir = Vector2Int.left;
            if(StageManager.Instance.PlayerMove(currentDir, CurrentPos))
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }
    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
