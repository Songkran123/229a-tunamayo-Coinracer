using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    private int score = 0;

    public int targetScore = 5;           // คะแนนที่ต้องถึง
    public GameObject winTextObject;        // ข้อความ “You Win” หรือ UI แจ้งจบเกม

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        if (score >= targetScore)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Win! 🎉");

        if (winTextObject != null)
            winTextObject.SetActive(true);
        Time.timeScale = 1f;

        // หรือถ้าจะโหลดฉากใหม่:
        // SceneManager.LoadScene("ชื่อฉากถัดไป");

        // หรือหยุดเกม (ใน Editor เท่านั้น):
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}