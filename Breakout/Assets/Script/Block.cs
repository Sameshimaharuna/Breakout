using UnityEngine;

public class Block : MonoBehaviour
{
    // ゲームマネージャーへの参照
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        // Ballタグのオブジェクトに接触したら
        if (collision.gameObject.CompareTag("Ball"))
        {
            // スコア更新
            if (gameManager != null)
            {
                gameManager.AddScore(10);
            }

            // 自分（ブロック）を削除
            Destroy(gameObject);
        }
    }
}
