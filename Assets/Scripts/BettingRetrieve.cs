using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BettingRetrieve : MonoBehaviour {

    public bool _running;
    public bool _visible;

    public Text playerOneText;
    public Text playerTwoText;
    public Text url;

    private List<int> playerOneScoreHistory;
    private List<int> playerTwoScoreHistory;

	// Use this for initialization
	void Start () {
        StartCoroutine(Retrieve());

        playerOneText.gameObject.SetActive(false);
        playerTwoText.gameObject.SetActive(false);
        url.gameObject.SetActive(false);

        playerOneScoreHistory = new List<int>();
        playerTwoScoreHistory = new List<int>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.B))
        {
            playerOneText.gameObject.SetActive(!playerOneText.gameObject.activeSelf);
            playerTwoText.gameObject.SetActive(!playerTwoText.gameObject.activeSelf);

            url.gameObject.SetActive(!url.gameObject.activeSelf);
        }
	}

    IEnumerator Retrieve()
    {
        while(_running)
        {
            UnityWebRequest www = UnityWebRequest.Get("https://gd6ve8v2mk.execute-api.eu-west-1.amazonaws.com/prod/SendPlayerScore/");
            yield return www.Send();

            if (www.isNetworkError)
                Debug.Log(www.error);
            else
            {
                playerOneText.text = "Bets: " + BettingData.CreateFromJSON(www.downloadHandler.text).Player2;
                playerTwoText.text = "Bets: " + BettingData.CreateFromJSON(www.downloadHandler.text).Player1;

                playerOneScoreHistory.Add(BettingData.CreateFromJSON(www.downloadHandler.text).Player2);
                playerTwoScoreHistory.Add(BettingData.CreateFromJSON(www.downloadHandler.text).Player1);
            }

            yield return new WaitForSeconds(1);
        }
    }
}

[System.Serializable]
public class BettingData
{
    public int Player1;
    public int Player2;

    public static BettingData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<BettingData>(jsonString);
    }
}
