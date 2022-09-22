using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUi: MonoBehaviour
{
    public Text highScoreText;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("high_score"))
        {
            highScoreText.text = "High Score " + "0";
        }
        else
        {
            highScoreText.text ="High Score "+PlayerPrefs.GetInt("high_score").ToString();
        }
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
