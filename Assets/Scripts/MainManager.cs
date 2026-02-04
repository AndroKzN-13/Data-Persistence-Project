using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = $" Score : {DataManager.Instance.playerName} : {m_Points}";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if(DataManager.Instance != null)
        {
            bestScoreText.text = $"Best Score : {DataManager.Instance.bestPlayerName} : {DataManager.Instance.bestScore}";
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {DataManager.Instance.playerName} : {m_Points}";
    }

    public void GameOver()
    {
        GameOverText.SetActive(true);

        if(m_Points > DataManager.Instance.highScores[9].bestScore)
        {
            DataManager.Instance.AddHighScore(DataManager.Instance.playerName, m_Points);
        }

        if(m_Points > DataManager.Instance.bestScore)
        {
            DataManager.Instance.bestScore = m_Points;
            DataManager.Instance.bestPlayerName = DataManager.Instance.playerName;
            bestScoreText.text = $"Best Score : {DataManager.Instance.bestPlayerName} : {DataManager.Instance.bestScore}";
            DataManager.Instance.SaveProfile();
            DataManager.Instance.AddHighScore(DataManager.Instance.bestPlayerName, DataManager.Instance.bestScore);
        } 
    }
}
