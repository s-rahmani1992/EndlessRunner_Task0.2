using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverDialog : MonoBehaviour
{
    [SerializeField]
    Text gameOverTxt;
    // Start is called before the first frame update
    
    public void Popup(int score, bool highScore)
    {
        gameOverTxt.text = (highScore ? "New Highscore!\nYour Score: " + score : "Your Score: " + score + "\nHigh Score: " + PlayerPrefs.GetInt("high"));
        gameObject.SetActive(true);
    }
}
