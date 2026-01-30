using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public float spawnRateStart = 2.0f;
    [SerializeField] public float spawnRateEnd = 4.0f;
    [SerializeField] public float decayRate = 0.95f;
    [SerializeField] public Transform[] spawnPoints; // Assign these in the Inspector

    [SerializeField] public float _nextSpawnTime;

    void Update()
    {
        // Only spawn if the game is active
        //if (GameManager.Instance != null && !GameManager.Instance.isGameActive) return;

        if (Time.time > _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + Random.Range(spawnRateStart,spawnRateEnd);
            spawnRateStart= spawnRateStart*decayRate;
            spawnRateEnd= spawnRateEnd*decayRate;
            Debug.Log(_nextSpawnTime-Time.time);
        }
    }

    void SpawnEnemy()
    {
        // Pick a random spawn point from your array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefab, selectedPoint.position, selectedPoint.rotation);
    }
}
