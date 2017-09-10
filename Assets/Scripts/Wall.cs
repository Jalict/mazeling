using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health;
    public Material[] wallMaterials;

    private Renderer rend;

    void Start()
    {
        health = 1;     // Full health

        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {

    }

    public void Hit()
    {
        health -= 0.25f;

        if (health <= 0)
            gameObject.SetActive(false);    //No need to destroy, just disable it

        //TODO change first -> last material depending on health
        if (health <= 0.25f)
            rend.material = wallMaterials[3];
        else if (health <= 0.5f)
            rend.material = wallMaterials[2];
        else if (health <= 0.75f)
            rend.material = wallMaterials[1];
        else if (health < 1f)
            rend.material = wallMaterials[0];
    }
}