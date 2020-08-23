using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private new Rigidbody rigidbody;
  [SerializeField] private float moveSpeed;
  [SerializeField] private float rotateSpeed;
  [SerializeField] private LayerMask rotationMask;

  [Space]
  [SerializeField] private Animator animatorController;

  private Vector3 direction = Vector3.zero;

  private void Start()
  {

  }

  private void Update()
  {
    direction = new Vector3(
      Input.GetAxis("Horizontal"), 0f,
      Input.GetAxis("Vertical")
    ).normalized;

    animatorController.SetBool("isWalking", direction.sqrMagnitude > 0f);
  }

  private void FixedUpdate()
  {
    rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime);

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, float.NegativeInfinity, rotationMask))
    {
      Vector3 difference = hit.point - rigidbody.position;

      Vector3 flatten = new Vector3(
        difference.x, 0f,
        difference.y
      );

      Quaternion target = Quaternion.LookRotation(flatten);

      rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, target, rotateSpeed * Time.fixedDeltaTime));
    }
  }
}
