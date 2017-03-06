using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollowTarget : MonoBehaviour {

  // The game object to follow
  public GameObject target;

  // The Rigidbody of the target
  private Rigidbody targetRB;

  // How fast the camera attempts to move towards proper positioning
  private float cameraMoveSpeed = 5f;

  public void Start() {
    // Finding components
    targetRB = target.GetComponent<Rigidbody>();
    Assert.IsNotNull(targetRB, "Failed to locate a Rigidbody on target");
  }

  public void LateUpdate() {
    // Grab the targets position. Offset it as a function of the target's speed
    Vector3 targetPosition = target.transform.position;
    targetPosition.y += OffsetY;
    targetPosition.z += OffsetZ;

    // Move our current position towards the target, but not instantly
    Vector3 currentPosition = transform.position;
    transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * cameraMoveSpeed);

    // Make sure we are looking at the target
    transform.LookAt(target.transform);
  }

  // How far above the target we should be
  private float OffsetY { get { return 20; } }

  // How far behind the target we should be. Adjusts as a function of speed of target
  private float OffsetZ { get { return -30 - targetRB.velocity.magnitude; } }

}
