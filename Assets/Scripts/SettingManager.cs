using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SettingManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Toggle onToggle;
    public Toggle offToggle;
    public Slider volumeSlider;
    public List<Button> buttonGroup;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    private Button clickedButton;
    public Color activeColor = new Color(0.44f, 0.86f, 0.85f);
    public Color normalColor = new Color(1f, 1f, 1f, 1f);
    private bool _isActive = false;
    private EventSystem eventSystem;
    private GameObject lastSelected = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataManager.Instance.LoadSetting();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        foreach(Button button in buttonGroup)
        {
            button.onClick.AddListener(() => UpdateVisuals(button));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(eventSystem != null)
        {
            if(eventSystem.currentSelectedGameObject != null)
            {
                lastSelected = eventSystem.currentSelectedGameObject;
            }
            else
            {
                eventSystem.SetSelectedGameObject(lastSelected);
            }
        }*/
    }

    public void CopySetting()
    {
        //DataManager.SettingData.volumeSlider = volumeSlider;
    }

    public void ToggleButtonState()
    {
        //if(easyButton.isSelected == false)
        foreach(Button button in buttonGroup)
        {
            _isActive = !_isActive;
            UpdateVisuals(button);
        }
    }

    public void UpdateVisuals(Button clickedButton)
    {
        foreach(Button button in buttonGroup)
        {
            ColorBlock colors = button.colors;

            if(button == clickedButton)
            {
                colors.normalColor = activeColor;
                colors.highlightedColor = activeColor;
                colors.selectedColor = activeColor;
            }
            else
            {
                colors.normalColor = normalColor;
                colors.highlightedColor = normalColor;
                colors.selectedColor = normalColor;
            }
            
            button.colors = colors;
        }
        
    }
}
