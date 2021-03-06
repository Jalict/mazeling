﻿using System;
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
    public Screenshake shake;
    public Image playerHealthImg;
    public Image playerHealthImgSpec;

    public AudioClip[] hurt;

    void Start()
    {
        health = 1;
    }

    public void Hit()
    {
        health -= hitDamage;

        shake.Shake(0.25f, 0.25f);
        AudioSource.PlayClipAtPoint(hurt[UnityEngine.Random.Range(0, hurt.Length)], transform.position);

        playerHealthImg.rectTransform.localScale = new Vector3(health, 1, 1);
        playerHealthImgSpec.rectTransform.localScale = new Vector3(health, 1, 1);

        if (health <= 0)
        {
            GameObject blood = Instantiate(bloodsplat, transform.position, Quaternion.identity);
            Destroy(blood, 2f);

            Respawn(Vector3.zero);

            if (GetComponent<PlayerMovement>().id == 2)
                GameSession.Instance.AddKill(2);
            else if(GetComponent<PlayerMovement>().id == 1)
                GameSession.Instance.AddKill(1);
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
