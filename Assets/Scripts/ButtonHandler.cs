using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public bool easySelected = true;
    public bool mediumSelected = false;
    public bool hardSelected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //easyButton.isPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleButton();
    }

    public void ToggleButton()
    {
        if(easySelected == true)
        {
            mediumSelected = false;
            hardSelected = false;
        }
        else if(mediumSelected == true)
        {
            easySelected = false;
            hardSelected = false;
        }
        else if(hardSelected == true)
        {
            easySelected = false;
            mediumSelected = false;
        }
    }
}
