using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextEditor : MonoBehaviour {

    Text text;
    int Score;
    int displayScore;

    // Use this for initialization
    void Start() {
        text = GetComponent<Text>();
    }


    void UpdateScore(int number)
    {
        Score = number;
        StartCoroutine(UpdateText());
    }

    private void OnEnable()
    {
        ScoreController.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreController.OnScoreChanged -= UpdateScore;
    }

    IEnumerator UpdateText()
    {
        while(displayScore < Score)
        {
            if(displayScore + 100 < Score)
            {
                displayScore += 100;
            }
            else if(displayScore + 10 < Score)
            {
                displayScore += 10;
            }
            else
            {
                displayScore++;
            }
           
            text.text = displayScore.ToString();
            yield return new WaitForEndOfFrame();
        }
        
    }
    
}
