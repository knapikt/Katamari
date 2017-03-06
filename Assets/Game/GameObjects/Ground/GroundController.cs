using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simple class representing the ground. Mainly for setting the material of the ground and having an object
// the player understands is the ground via the Tag

public class GroundController : MonoBehaviour {

  private void Awake() {
    gameObject.tag = Tag.Ground;

    var collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(ResourceConstant.Ground);

    var rb = gameObject.AddComponent<Rigidbody>();
    rb.isKinematic = true;
    rb.useGravity = false;
  }
}
