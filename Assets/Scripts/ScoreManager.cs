using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // Reference to the Text component that displays the score
    private int score = 0;  // The current score

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score display
        UpdateScoreDisplay();
    }

    // Update the score and display it
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    // Update the score text on the screen
    private void UpdateScoreDisplay()
    {
        scoreText.text = "x " + score.ToString();
    }
}
