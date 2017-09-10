using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {
    [Header("Settings")]
    public float health;        // (0-1) health of player
    public float hitDamage;     // (0-1) Amount of damage on hit

    [Header("References")]
    public GameObject bloodsplat;
    public Image playerHealthImg;
    public Image playerHealthImgSpec;

    void Start()
    {
        health = 1;
    }

    public void Hit()
    {
        health -= hitDamage;

        playerHealthImg.rectTransform.localScale = new Vector3(health, 1, 1);
        playerHealthImgSpec.rectTransform.localScale = new Vector3(health, 1, 1);

        if (health <= 0)
        {
            GameObject blood = Instantiate(bloodsplat, transform.position, Quaternion.identity);
            Destroy(blood, 2f);

            Respawn(Vector3.zero);

            if (GetComponent<PlayerMovement>().id == 0)
                GameSession.Instance.AddKill(1);
            else
                GameSession.Instance.AddKill(0);
        }
    }

    public void Respawn(Vector3 pos)
    {
        health = 1;

        if (pos == Vector3.zero)
            transform.position = MazeGenerator.Instance.GetRandomCorner();
        else
            transform.position = pos;

        playerHealthImg.rectTransform.localScale = new Vector3(1, 1, 1);
        playerHealthImgSpec.rectTransform.localScale = new Vector3(1, 1, 1);
        transform.position = MazeGenerator.Instance.GetRandomCorner();
        transform.LookAt(new Vector3(29f / 2f, 2f, 29f / 2f));
    }
}
