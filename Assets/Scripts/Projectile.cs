using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.CompareTag("Player"))
        {
            // Hit them
        }

        gameObject.SetActive(false);
    }
}
