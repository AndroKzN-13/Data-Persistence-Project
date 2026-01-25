using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public string PlayerName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
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
        DataManager.Instance.SaveName();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit;
        #endif
    }
}
