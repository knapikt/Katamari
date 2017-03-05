using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuilder : MonoBehaviour {

  private void Awake() {
    gameObject.tag = Tag.Ground;

    var collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);

    gameObject.AddComponent<GroundController>();
    var rb = gameObject.AddComponent<Rigidbody>();
    rb.isKinematic = true;
    rb.useGravity = false;
  }
}
