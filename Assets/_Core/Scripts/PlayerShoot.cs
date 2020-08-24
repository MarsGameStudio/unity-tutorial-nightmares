using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
  [SerializeField] private Transform firePoint;
  [SerializeField] private float timeBetweenShots = 0.15f;
  [SerializeField] private float damage = 25f;

  [Header("GFX: ")]
  [SerializeField] private float displayTime = 0.2f;
  [SerializeField] private ParticleSystem gunParticle;
  [SerializeField] private LineRenderer gunRenderer;
  [SerializeField] private Light gunLight;

  [Header("SFX: ")]
  [SerializeField] private AudioSource audioSource;

  private float fireCooldown = 0f;

  private void Update()
  {
    fireCooldown += Time.deltaTime;

    if (fireCooldown >= timeBetweenShots && Input.GetButtonDown("Fire"))
    {
      fireCooldown = 0f;

      audioSource.Play();

      gunParticle.Stop();
      gunParticle.Play();

      gunRenderer.SetPosition(0, firePoint.position);
      gunRenderer.enabled = true;

      gunLight.enabled = true;

      Ray ray = new Ray(
        firePoint.position,
        firePoint.forward
      );

      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
      {
        EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

        if (health &&
            health.IsDead() == false)
        {
          health.TakeDamage(damage, hit.point);
        }

        gunRenderer.SetPosition(1, hit.point);
      }
      else
      {
        gunRenderer.SetPosition(1, ray.origin + ray.direction * 100f);
      }
    }

    if (fireCooldown >= timeBetweenShots * displayTime) DisableEffects();
  }

  private void DisableEffects()
  {
    gunRenderer.enabled = false;
    gunLight.enabled = false;
  }
}
