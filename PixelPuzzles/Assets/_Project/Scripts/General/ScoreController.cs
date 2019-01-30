using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public static event Action<int> OnScoreChanged = delegate { };

    public static int currentScore = 0;

    public static void AddScore(int amountToAdd)
    {
        currentScore += amountToAdd;
        OnScoreChanged(currentScore);

    }

}
