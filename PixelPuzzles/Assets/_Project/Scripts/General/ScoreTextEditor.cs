using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTextEditor : MonoBehaviour {

    Text text;
    TextMeshProUGUI m_Text;
    int Score;
    int displayScore;

    // Use this for initialization
    void Start() {
        m_Text = GetComponent<TextMeshProUGUI>();
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

            // text.text = displayScore.ToString();
            m_Text.text = displayScore.ToString();
            yield return new WaitForEndOfFrame();
        }
        
    }
    
}
