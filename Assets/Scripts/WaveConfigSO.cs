using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject {
    [SerializeField] Transform pathPrefab;

    [Header("Enemies")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minSpawnTime = 0.2f;
    [SerializeField] List<GameObject> enemyPrefabs;

    public int GetEnemyCount() => enemyPrefabs.Count;
    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];
    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);
    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform point in pathPrefab) {
            waypoints.Add(point);
        }
        return waypoints;
    }
    public float GetMoveSpeed() => moveSpeed;
    public float GetRandomSpawnTime() {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }
}
