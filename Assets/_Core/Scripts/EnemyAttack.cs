using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  [SerializeField] private float timeBetweenAttacks = 0.5f;
  [SerializeField] private float damage = 10f;
  [SerializeField] private Animator animatorController;

  private float attackCooldown;

  private void Update()
  {
    attackCooldown += Time.deltaTime;
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.tag == "Player")
    {
      if (attackCooldown >= timeBetweenAttacks)
      {
        attackCooldown = 0f;

        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health &&
            health.IsDead() == false)
        {
          health.TakeDamage(damage);
        }

        if (health.IsDead()) animatorController.SetBool("shouldMove", false);
      }
    }
  }
}
