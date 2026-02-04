using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Linq;
public class HighScoresManager : MonoBehaviour
{
    public Text[] displayNames;
    public Text[] displayScores;
    public Transform[] scoreEntry;
    public Transform wrapper;
    public int maxScores = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayScoreEntry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayScoreEntry()
    {
        for(int i = 0; i < maxScores; i++)
        {
            displayNames[i].text = DataManager.Instance.highScores[i].bestPlayerName;
            displayScores[i].text = DataManager.Instance.highScores[i].bestScore.ToString();
        }
    }
}
