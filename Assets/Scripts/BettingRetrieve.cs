using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BettingRetrieve : MonoBehaviour {

    public bool _running;
    public bool _visible;

    public Text playerOne;
    public Text playerTwo;

	// Use this for initialization
	void Start () {
        StartCoroutine(Retrieve());

        playerOne.gameObject.SetActive(false);
        playerTwo.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.B))
        {
            playerOne.gameObject.SetActive(!playerOne.gameObject.activeSelf);
            playerTwo.gameObject.SetActive(!playerTwo.gameObject.activeSelf);
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
                playerOne.text = "Bets: " + BettingData.CreateFromJSON(www.downloadHandler.text).Player1;
                playerTwo.text = "Bets: " + BettingData.CreateFromJSON(www.downloadHandler.text).Player2;
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
