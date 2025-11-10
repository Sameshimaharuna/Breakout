using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // 経過時間
    private float elapsedTime = 0f;

    // TextMeshPro参照（Inspectorで設定）
    public TextMeshProUGUI timeText;

    void Update()
    {
        // 経過時間を加算
        elapsedTime += Time.deltaTime;

        // 分・秒・ミリ秒に変換して表示
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // TextMeshProに反映（例： 01:23.45）
        if (timeText != null)
        {
            timeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
