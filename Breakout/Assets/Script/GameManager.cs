using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 現在のスコア
    public int score = 0;

    // すべてのブロックを格納する配列
    public GameObject[] blocks;

    [SerializeField] Bar bar;       // Bar クラス
    [SerializeField] Timer timer;   // Timer クラス
    [SerializeField] Ball ball;  // Ball クラス

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
    public void StopObjects()
    {
        bar.StopMovement();
        timer.StopTimer();
        ball.StopBall();
    }
}