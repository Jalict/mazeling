using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int id;
    public float movementSpeed;
    public float rotationSpeed;
    public Color playerColor;
    public AudioClip powerup;

    private Rigidbody body;
    static private bool powerupActive;

    void Start ()
    {
	powerupActive = false;
        body = GetComponent<Rigidbody>();

        GetComponent<MeshRenderer>().material.color = playerColor;
    }
	
    void Update ()
    {
        float vertical = Input.GetAxis("Vertical_" + id);
        float horizontal = Input.GetAxis("Horizontal_" + id);

        if (horizontal != 0)
        {
            transform.Rotate(transform.up, horizontal * Time.deltaTime * rotationSpeed);
        }

        if (vertical != 0)
        {
            body.AddForce(transform.forward * Time.deltaTime * vertical * movementSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
	if (!powerupActive && other.name == "Hovering Object") {
		powerupActive = true;
		movementSpeed *= 2;
		StartCoroutine(PowerupTimeout());
            AudioSource.PlayClipAtPoint(powerup, transform.position,0.8f);
	}
    }

    public void Respawn()
    {
	if (powerupActive) {
		powerupActive = false;
		movementSpeed /= 2;
	}
    }

    IEnumerator PowerupTimeout()
    {
	    yield return new WaitForSeconds(8f);
	    powerupActive = false;
	    movementSpeed /= 2;
    }
}
