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
        if (health <= 0)
            gameObject.SetActive(false);    //No need to destroy, just disable it

        //TODO change first -> last material depending on health
        rend.material = wallMaterials[0];

        throw new System.NotImplementedException();
    }
}