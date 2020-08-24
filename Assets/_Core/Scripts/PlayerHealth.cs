using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] private Animator animatorController;
  [Space]

  [SerializeField] private float minHealth = 0f;
  [SerializeField] private float maxHealth = 100f;
  [SerializeField] private Slider slider;

  [Header("Damage: ")]
  [SerializeField] private Image image;
  [SerializeField] private float flashSpeed = 5f;
  [SerializeField] private Color flashColor = new Color(1f, 0f, 0f, 0.1f);

  [Header("SFX: ")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip hurtSound;
  [SerializeField] private AudioClip deathSound;

  [Space]
  [SerializeField] private Behaviour[] componentsToDisable;

  private float currentHealth;
  private bool isDamaged;

  private void Start()
  {
    currentHealth = maxHealth;
  }

  private void Update()
  {
    image.color = Color.Lerp(image.color, Color.clear, flashSpeed * Time.deltaTime);
  }

  public void TakeDamage(float amount)
  {
    currentHealth -= amount;

    image.color = flashColor;
    slider.value = currentHealth;

    if (currentHealth <= minHealth)
    {
      Death();
    }
    else
    {
      audioSource.clip = hurtSound;
      audioSource.Play();
    }
  }

  private void Death()
  {
    animatorController.SetTrigger("OnDeath");

    audioSource.clip = deathSound;
    audioSource.Play();

    foreach (var c in componentsToDisable) { c.enabled = false; }
  }

  public bool IsDead() => currentHealth <= minHealth;
}
