using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // GameManager参照（Inspectorで設定 or 自動検索）
    public GameManager gameManager;

    // TextMeshPro参照（Inspectorで設定）
    public TextMeshProUGUI scoreText;

    void Update()
    {
        // GameManagerのスコアをリアルタイム反映
        if (gameManager != null && scoreText != null)
        {
            scoreText.text = $"{gameManager.score}";
        }
    }
}