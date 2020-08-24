using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField] private Animator animatorController;
  [Space]

  [SerializeField] private float minHealth = 0f;
  [SerializeField] private float maxHealth = 100f;
  [SerializeField] private ParticleSystem hitParticle;
  [SerializeField] private float sinkSpeed = 2.5f;
  [SerializeField] private float score = 10f;

  [Header("SFX: ")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip hurtSound;
  [SerializeField] private AudioClip deathSound;

  [Space]
  [SerializeField] private Behaviour[] componentsToDisable;

  private float currentHealth;

  private void Start()
  {
    currentHealth = maxHealth;
  }

  public void TakeDamage(float amount, Vector3 point)
  {
    if (IsDead()) return;

    currentHealth -= amount;

    hitParticle.transform.position = point;
    hitParticle.Play();

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
    animatorController.SetTrigger("isDead");

    audioSource.clip = deathSound;
    audioSource.Play();

    GetComponent<CapsuleCollider>().isTrigger = true;
    GetComponent<Rigidbody>().isKinematic = true;

    foreach (var c in componentsToDisable) { c.enabled = false; }

    StartCoroutine(Sink());
  }

  private IEnumerator Sink()
  {
    float timer = 2f;

    while (timer > 0f)
    {
      transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
      timer -= Time.deltaTime;

      yield return new WaitForEndOfFrame();
    }

    Destroy(gameObject);
  }

  public bool IsDead() => currentHealth <= minHealth;
}
