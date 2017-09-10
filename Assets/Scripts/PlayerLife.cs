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

        transform.LookAt(new Vector3(29 / 2, 2, 29 / 2));

        playerHealthImg.rectTransform.localScale = new Vector3(1, 1, 1);
        playerHealthImgSpec.rectTransform.localScale = new Vector3(1, 1, 1);
        transform.position = MazeGenerator.Instance.GetRandomCorner();
        transform.LookAt(new Vector3(29 / 2, 0, 29 / 2));
    }
}
