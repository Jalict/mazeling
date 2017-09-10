using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour {
    public Direction movingDirection;
    public Space movingSpace;
    private bool enabled;

    private Transform objectMoved;
    
    // Use this for initialization
    void Start ()
    {
        objectMoved = transform.GetChild(0).transform;
	enabled = true;
    }
	
    // Update is called once per frame
    void Update ()
    {
        Vector3 position = objectMoved.position;
        position.y = enabled
                ? Mathf.Sin(Time.time) * 0.2f
                : -100; // hack, should really disable renderer...
        objectMoved.position = position;
    }

    Vector3 GetDirectionVector(Space space)
    {
        Vector3 vec = Vector3.zero;

        switch (movingSpace)
        {
            case Space.Global:
                if (movingDirection == Direction.UP)
                    vec = Vector3.up;
                else if (movingDirection == Direction.LEFT)
                    vec = Vector3.left;
                else if (movingDirection == Direction.FORWARD)
                    vec = Vector3.forward;
                break;
            case Space.Local:
                if (movingDirection == Direction.UP)
                    vec = transform.up;
                else if (movingDirection == Direction.LEFT)
                    vec = -transform.right;
                else if (movingDirection == Direction.FORWARD)
                    vec = transform.forward;
                break;
            default:
                throw new System.IndexOutOfRangeException("Space set to " + (int)movingSpace);
        }

        return vec;
    }

    void OnTriggerEnter(Collider other) {
        if (enabled && (other.gameObject.name == "Player Blue" || other.gameObject.name == "Player Red")) {
	    enabled = false;
	    StartCoroutine(respawnPowerup());
        }
    }

    IEnumerator respawnPowerup()
    {
	    yield return new WaitForSeconds(8f);
	    enabled = true;
    }
}

public enum Direction
{
    UP,
    LEFT,
    FORWARD
}

public enum Space
{
    Global,
    Local
}
