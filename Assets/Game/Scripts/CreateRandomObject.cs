using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateRandomObject : MonoBehaviour
{
    [SerializeField] private PrefabCollection[] prefabs;
    [SerializeField] private float velocityOffset;
    [SerializeField] private float velocityNoise;
    [SerializeField] private Transform spawnLocation;
    
    
    public void SpawnRandomObject()
    {
        int index = Random.Range(0, prefabs.Length);
        foreach (PrefabIntPair pair in prefabs[index].prefabs)
        {
            for (int i = 0; i < pair.count; i++)
            {
                GameObject newRB = Instantiate(pair.prefab, spawnLocation.position, Random.rotation);
                Rigidbody rb = newRB.GetComponent<Rigidbody>();
                if (rb)
                    rb.AddForce(-1 * velocityOffset * spawnLocation.right + velocityNoise * Random.insideUnitSphere,
                        ForceMode.VelocityChange);
            }
        }
    }
}
