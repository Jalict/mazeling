using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour {
    public float movingSpeed;
    public float movingAmount;
    public Direction movingDirection;
    public Space movingSpace;

    [Range(0,1)]
    private float t;
    private Transform objectMoved;
    private bool tick;

	// Use this for initialization
	void Start ()
    {
        objectMoved = transform.GetChild(0).transform;

        t = 0;
        tick = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    Vector3 directionVector = GetDirectionVector(movingSpace) * movingAmount;

        objectMoved.position = Vector3.Lerp(transform.position - directionVector, transform.position + directionVector, t);

        if(tick) {
            t += Time.deltaTime * movingSpeed;
        } else {
            t -= Time.deltaTime * movingSpeed;
        }

        if ((t >= 1 && tick) || (t <= 0 && !tick))
            tick = !tick;

        Debug.Log(t);
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
