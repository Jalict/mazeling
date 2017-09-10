using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionSize;
    public GameObject explosionPrefab;    // Prefab used to show projectile hitting wall
    public AudioClip[] explosionClip;

    void OnEnable()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        // Create explosion particles
        GameObject explosion = Instantiate(explosionPrefab, other.contacts[0].point, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionClip[Random.Range(0, explosionClip.Length)], Camera.allCameras[2].transform.position);
        Destroy(explosion, 0.4f);

        // Hit objects nearby
        Collider[] nearbyObjects = Physics.OverlapSphere(other.contacts[0].point, explosionSize);
        foreach(Collider col in nearbyObjects)
        {
            switch(col.tag)
            {
                case "Player":
                    col.GetComponent<PlayerLife>().Hit();
                    //TODO Add force to player on explosion?
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

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionSize);
    }
}
