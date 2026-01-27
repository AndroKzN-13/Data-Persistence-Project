using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField nameField;
    public Text bestScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestScoreText.text = $"Best Score : {DataManager.Instance.bestPlayerName} : {DataManager.Instance.bestScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        DataManager.Instance.playerName = nameField.text;
    }

    public void HighScorePage()
    {
        SceneManager.LoadScene(2);
    }

    public void SettingPage()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        DataManager.Instance.SaveProfile();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit;
        #endif
    }

    
}
