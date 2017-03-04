using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableBuilder : MonoBehaviour {

  private void Awake() {
    gameObject.tag = Tag.Attachable;

    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);

    gameObject.AddComponent<Rigidbody>();
  }
}
