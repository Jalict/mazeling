using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionSize;
    public GameObject explosionPrefab;    // Prefab used to show projectile hitting wall

    void OnCollisionEnter(Collision other)
    {
        // Create explosion particles
        GameObject explosion = Instantiate(explosionPrefab, other.contacts[0].point, Quaternion.identity);
        Destroy(explosion, 0.4f);

        // Hit objects nearby
        Collider[] nearbyObjects = Physics.OverlapSphere(other.contacts[0].point, explosionSize);
        foreach(Collider col in nearbyObjects)
        {
            switch(col.tag)
            {
                case "Player":
                    col.GetComponent<PlayerLife>().Hit();
                    break;
                case "Wall":
                    col.GetComponent<Wall>().Hit();
                    break;
                default:
                    break;
            }
        }

        // Make projectile go away
        gameObject.SetActive(false);
    }
}
