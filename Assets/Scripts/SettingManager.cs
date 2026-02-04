using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioSource audio;
    private bool isEnabled = true;
    private Toggle onToggle;
    private Toggle offToggle;
    private Button easyButton;
    private Button mediumButton;
    private Button hardButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        onToggle = GameObject.Find("OnToggle").GetComponent<Toggle>();
        offToggle = GameObject.Find("OffToggle").GetComponent<Toggle>();
        easyButton = GameObject.Find("Easy").GetComponent<Button>();
        mediumButton = GameObject.Find("Medium").GetComponent<Button>();
        hardButton = GameObject.Find("Hard").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        if(isEnabled == true)
        {
            //audio.PlayOneShot(AudioClip);
            //offToggle.disabled;
        }
    }
}
