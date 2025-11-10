using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 現在のスコア
    public int score = 0;

    // すべてのブロックを格納する配列
    public GameObject[] blocks;

    // 停止させたいオブジェクトたち（ボールとかBarとか）
    public GameObject[] stopObjects;

    void Update()
    {
        // ブロックが全て消えたかチェック
        bool allDestroyed = true;
        foreach (GameObject block in blocks)
        {
            if (block != null)
            {
                allDestroyed = false;
                break;
            }
        }

        // 全部消えたら停止処理
        if (allDestroyed)
        {
            StopObjects();
        }
    }

    // スコアを加算するメソッド（Blockから呼ばれる）
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    // 動作停止処理
    private void StopObjects()
    {
        foreach (GameObject obj in stopObjects)
        {
            if (obj != null)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }

                // ObjectMoverとかのスクリプトも止める
                MonoBehaviour mover = obj.GetComponent<MonoBehaviour>();
                if (mover != null)
                {
                    mover.enabled = false;
                }
            }
        }

        Debug.Log("全ブロックが消えたので動作停止しました。");
    }
}