using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 5f;                // 移動速度
    private Vector3 moveDirection;              // 現在の進行方向
    // GameManager参照（Inspectorで設定 or 自動検索）
    public GameManager gameManager;
    private Rigidbody rb;
    private bool isStopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 初期は下方向へ移動
        moveDirection = Vector3.down;
    }

    public void StopBall()
    {
        isStopped = true;
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if (isStopped) return;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bar・Wallタグのいずれかに接触したらランダムな方向へ変更
        if (collision.gameObject.CompareTag("Bar") ||
            collision.gameObject.CompareTag("Wall"))
        {
            Vector3 normal = collision.contacts[0].normal;

            // まずは物理的に正しい反射方向を取得
            Vector3 reflectDir = Vector3.Reflect(moveDirection, normal);

            // 小さなランダム角度（±15度）で調整
            float randomAngle = Random.Range(-15f, 15f);
            Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.up);

            // 回転後の新しい方向
            moveDirection = rotation * reflectDir;
            moveDirection.Normalize();
        }

        // Dedタグのオブジェクトに接触したら停止対象を止める
        else if (collision.gameObject.CompareTag("Ded"))
        {
            gameManager.StopObjects();
        }
    }
}