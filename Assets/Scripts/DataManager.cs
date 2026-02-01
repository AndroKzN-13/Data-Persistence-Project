using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;
    public int score;
    public string bestPlayerName;
    public int bestScore;
    public InputField nameField;
    public Text bestScoreText;
    public Text highScoreList;

    public List<SaveData> highScores = new List<SaveData>();
    private const int maxScores = 10;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadProfile();
        DisplayHighScores();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
        public SaveData(string name, int newScore)
        {
            bestPlayerName = name;
            bestScore = newScore;
        }
    }

    [System.Serializable]
    public class HighScoreListWrapper
    {
        public List<SaveData> highScores;
        public HighScoreListWrapper(List<SaveData> scores)
        {
            highScores = scores;
        }
    }

    public void SaveProfile()
    {
        SaveData data = new SaveData(bestPlayerName, bestScore);
        data.bestPlayerName = bestPlayerName;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadProfile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
            bestScoreText.text = $"Best Score: {bestPlayerName} : {bestScore}";
        }
    }

    public void AddHighScore(string bestPlayerName, int bestScore)
    {
        DisplayHighScores();
        highScores.Add(new SaveData(bestPlayerName, bestScore));
        highScores = highScores.OrderByDescending(s => s.bestScore).Take(maxScores).ToList();

        string json = JsonUtility.ToJson(new HighScoreListWrapper(highScores), true);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void DisplayHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreListWrapper wrapper = JsonUtility.FromJson<HighScoreListWrapper>(json);
            highScores = wrapper.highScores;
        }
    }
}
