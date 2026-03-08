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
    public SettingManager settingManager;
    public Toggle musicEnabled;
    public Slider volumeSlider;
    public Button[] difficultyButton;
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
        LoadSetting();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(displayNames[i].text = highScores[i].bestPlayerName);
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

    [System.Serializable]
    public class SettingData
    {
        public bool musicEnabled = true;
        public float volumeSlider = 0.25f;
        public int difficultyButton = 0;
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

    public void SaveSetting()
    {
        SettingData setting = new SettingData();
        string json = JsonUtility.ToJson(setting, true);
        File.WriteAllText(Application.persistentDataPath + "/setting.json", json);
        Debug.Log("Saved: music enabled = " + musicEnabled);
    }

    public void LoadSetting()
    {
        string path = Application.persistentDataPath + "/setting.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingData setting = JsonUtility.FromJson<SettingData>(json);
            musicEnabled.isOn = setting.musicEnabled;
            volumeSlider.value = setting.volumeSlider;
            UpdateDifficultyUI();
        }
    }

    public void SetMusic(bool isOn)
    {
        SettingData setting = new SettingData();
        setting.musicEnabled = isOn;
        SaveSetting();
    }

    public void SetVolume(float value)
    {
        SettingData setting = new SettingData();
        setting.volumeSlider = value;
        SaveSetting();
    }

    public void SetDifficulty(int difficultyButton)
    {
        SettingData setting = new SettingData();
        setting.difficultyButton = difficultyButton;
        SaveSetting();
        UpdateDifficultyUI();
    }

    public void ApplyDifficulty(int difficulty)
    {
        if(difficulty == 0)
        {
            Time.timeScale = 1.0f;
        }
        else if(difficulty == 1)
        {
            Time.timeScale = 1.5f;
        }
        else if(difficulty == 2)
        {
            Time.timeScale = 2.0f;
        }
    }

    public void UpdateDifficultyUI()
    {
        SettingData setting = new SettingData();
        for(int i = 0; i < difficultyButton.Length; i++)
        {
            difficultyButton[i].interactable = (i != setting.difficultyButton);
        }
    }
}
