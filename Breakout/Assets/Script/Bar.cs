using UnityEngine;

public class Bar : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private int moveDir = 0;      // -1：左、1：右、0：停止
    private int blockedDir = 0;   // 進行禁止方向

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ←キーで左、→キーで右（ただし進行禁止方向は無視）
        if (Input.GetKey(KeyCode.LeftArrow) && blockedDir != -1)
        {
            moveDir = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && blockedDir != 1)
        {
            moveDir = 1;
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = 0;
        }

        rb.linearVelocity = new Vector3(moveDir * moveSpeed, rb.linearVelocity.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 移動を停止
            rb.linearVelocity = Vector3.zero;

            // ぶつかった方向を進行禁止に設定
            if (moveDir > 0)
                blockedDir = 1;   // 右方向をブロック
            else if (moveDir < 0)
                blockedDir = -1;  // 左方向をブロック

            moveDir = 0;
            Debug.Log("Wallにぶつかって停止。方向ロック！");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁から離れたら再び移動可能に
            blockedDir = 0;
            Debug.Log("Wallから離れたので移動解除！");
        }
    }
}
