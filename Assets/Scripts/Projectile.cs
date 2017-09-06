using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody body;

	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Hit them
        }

        gameObject.SetActive(false);
    }
}
