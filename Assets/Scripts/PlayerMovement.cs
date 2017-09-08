using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int id;
    public float movementSpeed;
    public float rotationSpeed;
    public Color playerColor;

    private Rigidbody body;

	void Start ()
    {
        body = GetComponent<Rigidbody>();

        GetComponent<MeshRenderer>().material.color = playerColor;
	}
	
	void Update ()
    {
        float vertical = Input.GetAxis("Vertical_" + id);
        float horizontal = Input.GetAxis("Horizontal_" + id);

        if(horizontal != 0)
        {
            transform.Rotate(transform.up, horizontal * Time.deltaTime * rotationSpeed);
        }

        if(vertical != 0)
        {
            body.velocity = transform.forward * Time.deltaTime * vertical * movementSpeed;
        }
	}
}
