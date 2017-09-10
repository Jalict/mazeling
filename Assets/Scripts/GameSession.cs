using System.Collections;
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

    private int playerOneScore;
    private int playerTwoScore;

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
        RestartRound();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeLeft = (int)(totalRoundTime - (Time.time - timeStarted));

        timeText.text = "Time Left\n"+timeLeft;

        if(timeLeft <= 0)
            RestartRound();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Delete))
            RestartRound();
	}

    void RestartRound()
    {
        timeStarted = Time.time;

        playerOneLife.Respawn(new Vector3(1,2,1));
        playerTwoLife.Respawn(new Vector3(29, 2, 29));
    }

    public void AddKill(int i)
    {
        if (i == 0)
            playerOneScore++;
        else if (i == 1)
            playerTwoScore++;
    }
}
