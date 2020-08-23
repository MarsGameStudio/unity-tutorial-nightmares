using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
  [SerializeField] private NavMeshAgent navMeshAgent;

  private Transform player;

  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }

  private void Update()
  {
    navMeshAgent.SetDestination(player.position);
  }
}
