using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionPrefab;    // Prefab used to show projectile hitting wall

    private GameObject explosion;         // Instance of hit wall object (Instantiated from the start, don't delete!)
    private Explosion particles;

    void Awake()
    {
        explosion = Instantiate(explosionPrefab);
        particles = explosion.GetComponent<Explosion>();

        explosion.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        explosion.SetActive(true);
        StartCoroutine(particles.Explode(other));
        gameObject.SetActive(false);    // Disable projectile (Not visible, don't move it any longer)
    }
}
