using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateRandomObject : MonoBehaviour
{
    [SerializeField] private PrefabCollection[] prefabs;
    [SerializeField] private float velocityOffset;
    [SerializeField] private float velocityNoise;
    [SerializeField] private float positionNoise;
    [SerializeField] private Transform spawnLocation;
    private float cooldownUntil;
    [SerializeField] private float cooldownTime;
    [SerializeField] private GameObject prompt;

    private void Awake()
    {
        cooldownUntil = Time.time;
    }

    private void Update()
    {
        prompt.SetActive(Time.time >= cooldownUntil);
    }

    public void SpawnRandomObject()
    {
        if (Time.time < cooldownUntil) return;
        
        cooldownUntil = Time.time + cooldownTime;
        
        int index = Random.Range(0, prefabs.Length);
        foreach (PrefabIntPair pair in prefabs[index].prefabs)
        {
            for (int i = 0; i < pair.count; i++)
            {
                GameObject newRB = Instantiate(pair.prefab, spawnLocation.position + positionNoise * Random.insideUnitSphere, Random.rotation);
                Rigidbody rb = newRB.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(-1 * velocityOffset * spawnLocation.right + velocityNoise * Random.insideUnitSphere,
                        ForceMode.VelocityChange);
                    rb.isKinematic = false;
                }
            }
        }
    }
}
