using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    private ParticleSystem particles;

	// Use this for initialization
	void Start ()
    {
        particles = GetComponent<ParticleSystem>();
	}

    public IEnumerator Explode(Collision other)
    {
        // Reset time for particles
        particles.Clear();

        Debug.Log(particles);

        // Set explosion location to contactpoint
        transform.position = other.contacts[0].point;

        // Show explosion & Disable after x
        particles.Play();
        yield return new WaitForSeconds(0.4f);
        particles.Stop();
        gameObject.SetActive(false);
    }
}
