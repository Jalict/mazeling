﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {
    public float health;        // (0-1) health of player
    public float hitDamage;     // (0-1) Amount of damage on hit

    void Start()
    {
        health = 1;
    }

    public void Hit()
    {
        health -= hitDamage;

        if(health <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {

    }
}
