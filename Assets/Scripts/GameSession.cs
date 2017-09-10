﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    [Header("Settings")]
    public float totalRoundTime;

    [Header("References")]
    public Text timeText;
    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    public PlayerLife playerOneLife;
    public PlayerLife playerTwoLife;

    private float timeStarted;
    private int timeLeft;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    // Singleton
    private static GameSession instance;
    public static GameSession Instance
    {
        get { return instance; }
    }
    void Awake() { instance = this; }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeLeft = (int)(totalRoundTime - (Time.time - timeStarted));

        timeText.text = "Time Left\n"+timeLeft;

        if(timeLeft <= 0)
            RestartRound();

        playerOneScoreText.text = "Score\n" + playerOneScore;
        playerTwoScoreText.text = "Score\n" + playerTwoScore;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Delete))
            RestartRound();
	}

    void RestartRound()
    {
        Application.LoadLevel(0);
    }

    public void AddKill(int i)
    {
        if (i == 0)
            playerOneScore++;
        else if (i == 1)
            playerTwoScore++;
    }
}