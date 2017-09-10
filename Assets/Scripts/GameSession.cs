using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    [Header("Settings")]
    public float totalRoundTime;

    [Header("References")]
    public Text timeText;
    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    public PlayerLife playerOneLife;
    public PlayerMovement playerOneMovement;
    public PlayerLife playerTwoLife;
    public PlayerMovement playerTwoMovement;
    public AudioClip beep;
    public GameObject standardUI1, standardUI2;
    public GameObject endUI1, endUI2;

    public BettingRetrieve bets;

    public Text finalBlueD1, finalBlueD2;
    public Text finalRedD1, finalRedD2;

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
        timeStarted = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeLeft = (int)(totalRoundTime - (Time.time - timeStarted));

        timeText.text = "Time Left\n"+timeLeft;

        if(timeLeft <= 0)
            StartCoroutine(EndRound());

        playerOneScoreText.text = "Score\n" + playerOneScore;
        playerTwoScoreText.text = "Score\n" + playerTwoScore;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Delete))
            StartCoroutine(RestartRound());
	}

    IEnumerator EndRound()
    {
        playerOneMovement.enabled = false;
        playerTwoMovement.enabled = false;

        standardUI1.SetActive(false);
        standardUI2.SetActive(false);
        endUI1.SetActive(true);
        endUI2.SetActive(true);

        finalBlueD1.text = "BLUE PLAYER\nScore: " + playerTwoScore + "\nBets: " + bets.playerTwoBets;
        finalBlueD2.text = "BLUE PLAYER\nScore: " + playerTwoScore + "\nBets: " + bets.playerTwoBets;
        finalRedD1.text = "RED PLAYER\nScore: " + playerOneScore + "\nBets: " + bets.playerOneBets;
        finalRedD2.text = "RED PLAYER\nScore: " + playerOneScore + "\nBets: " + bets.playerOneBets;

        yield return new WaitForSeconds(10);

        StartCoroutine(RestartRound());
    }

    IEnumerator RestartRound()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);

        yield return new WaitForSeconds(2);

        timeStarted = Time.time;

        playerOneLife.Respawn(new Vector3(1,2,1));
	    playerOneMovement.Respawn();
        playerTwoLife.Respawn(new Vector3(29, 2, 29));
	    playerTwoMovement.Respawn();
    }

    public void AddKill(int i)
    {
        if (i == 1)
            playerOneScore++;
        else if (i == 2)
            playerTwoScore++;

        AudioSource.PlayClipAtPoint(beep, Vector3.zero);
    }
}
