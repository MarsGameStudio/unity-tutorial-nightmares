using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private GameObject[] enemies;
  [SerializeField] private Transform[] points;
  [SerializeField] private float timeBetweenSpawns;

  private PlayerHealth playerHealth;

  private void Awake()
  {
    InvokeRepeating("Spawn", timeBetweenSpawns, timeBetweenSpawns);
  }

  private void Start()
  {
    playerHealth = FindObjectOfType<PlayerHealth>();
  }

  private void Spawn()
  {
    if (playerHealth.IsDead()) return;

    GameObject enemy = enemies[Random.Range(0, enemies.Length)];
    Transform point = points[Random.Range(0, points.Length)];

    Instantiate(enemy,
      point.position,
      point.rotation
    );
  }
}
