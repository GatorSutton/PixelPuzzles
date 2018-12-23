using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextEditor : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	

    void UpdateText(int number)
    {
        text.text = number.ToString();
    }

    private void OnEnable()
    {
        ScoreController.OnScoreChanged += UpdateText;
    }

    private void OnDisable()
    {
        ScoreController.OnScoreChanged -= UpdateText;
    }
}
