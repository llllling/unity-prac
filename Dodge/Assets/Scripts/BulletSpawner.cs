using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    /*
     * 
     * 
     */
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    private Transform targetTransform;
    private float spawnRate;
    private float timeAfterSpawn;

    private float getRandomSpawnRate()
    {
        return Random.Range(spawnRateMin, spawnRateMax);
    }

    private void clearTimeAfterSpawn()
    {
        timeAfterSpawn = 0f;
    }

    void Start()
    {
        clearTimeAfterSpawn();

        spawnRate = getRandomSpawnRate();

        targetTransform = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn < spawnRate) return;

        clearTimeAfterSpawn();

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        bullet.transform.LookAt(targetTransform);

        spawnRate = getRandomSpawnRate();
    }
}
