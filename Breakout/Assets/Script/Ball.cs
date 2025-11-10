using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 5f;                // 移動速度
    private Vector3 moveDirection;              // 現在の進行方向
    public GameObject[] stopTargets;            // 止めたいオブジェクトたち（Inspectorで登録）

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 初期は下方向へ移動
        moveDirection = Vector3.down;
    }

    void FixedUpdate()
    {
        // Rigidbodyの物理挙動で移動
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bar・Wall・Ballタグのいずれかに接触したらランダムな方向へ変更
        if (collision.gameObject.CompareTag("Bar") ||
            collision.gameObject.CompareTag("Wall") ||
            collision.gameObject.CompareTag("Ball"))
        {
            // 接触面の平均法線を計算
            Vector3 avgNormal = Vector3.zero;
            foreach (var contact in collision.contacts)
            {
                avgNormal += contact.normal;
            }
            avgNormal.Normalize();

            // まずは物理的に正しい反射方向を求める
            Vector3 reflectDir = Vector3.Reflect(moveDirection, avgNormal);

            // 反射角が浅すぎた場合の補正（ほぼ水平なら少し上げる）
            if (Mathf.Abs(reflectDir.y) < 0.2f)
            {
                reflectDir.y = Mathf.Sign(reflectDir.y) * 0.3f;
            }

            // タグごとの挙動調整
            if (collision.gameObject.CompareTag("Bar"))
            {
                // Barに当たったら、上方向成分を強調（確実に上に跳ねる）
                reflectDir.y = Mathf.Abs(reflectDir.y) + 0.4f;
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                // 壁なら、上下成分を維持してランダムに横ズラし
                float randomAngle = Random.Range(-10f, 10f);
                Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.up);
                reflectDir = rotation * reflectDir;
            }

            // 正規化して新しい進行方向を設定
            moveDirection = reflectDir.normalized;
        }

        // Dedタグのオブジェクトに接触したら停止対象を止める
        else if (collision.gameObject.CompareTag("Ded"))
        {
            foreach (GameObject obj in stopTargets)
            {
                if (obj != null)
                {
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero;
                    }
                }
            }
        }
    }
}