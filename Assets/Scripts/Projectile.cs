using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionPrefab;    // Prefab used to show projectile hitting wall

    void OnCollisionEnter(Collision other)
    {
        GameObject explosion = Instantiate(explosionPrefab, other.contacts[0].point, Quaternion.identity);
        Destroy(explosion, 0.4f);
        gameObject.SetActive(false);
    }
}
