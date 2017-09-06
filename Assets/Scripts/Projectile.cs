using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private Rigidbody body;

	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        body.MovePosition(transform.position + (transform.forward * projectileSpeed * Time.deltaTime));
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Hit them
        }

        Debug.Log(name + " hit " + other.gameObject.name);

        gameObject.SetActive(false);
    }
}
