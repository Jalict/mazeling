using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [Header("Settings")]
    public KeyCode key;                 // Key to shoot
    public float shootingSpeed;         // How fast you shoot (seconds)
    public float projectileSpeed;       // How fast your projectiles fly
    public int maxNumOfProjectiles;     // Maximum number of projectiles to be in the world (Object pooling)

    [Header("Assets")]
    public GameObject projectilePrefab; // Prefab to Instantiate for the projectile

    private GameObject[] projectiles;   // Projectiles that have been shot (Is object pooled)
    private int pIndex;                 // Projectile index (Used for object pooling)
    private float lastShotTime;         // The timestamp that last projectile was shot on (Used to determine when the next time that a projectile gets shot - See: shootingSpeed)

	// Use this for initialization
	void Start ()
    {
        projectiles = new GameObject[maxNumOfProjectiles];

        for (int i = 0; i < maxNumOfProjectiles; i++)
        {
            projectiles[i] = Instantiate(projectilePrefab);
            projectiles[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float time = Time.time;

		if(Input.GetKey(key) && time - lastShotTime >= shootingSpeed)
        {
            projectiles[pIndex].transform.position = transform.forward * 0.2f;

            projectiles[pIndex].SetActive(true);

            lastShotTime = time;
            pIndex++;   // Go to next projectile in the array for next "shot"
        }
	}
}
