using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] private float restartDelay = 5f;
  [SerializeField] private Animator animatorController;

  private PlayerHealth playerHealth;
  private float restartCooldown;

  private void Start()
  {
    playerHealth = FindObjectOfType<PlayerHealth>();
  }

  private void Update()
  {
    if (playerHealth.IsDead())
    {
      animatorController.SetTrigger("GameOver");

      if ((restartCooldown += Time.deltaTime) >= restartDelay) SceneManager.LoadScene(0);
    }
  }
}
