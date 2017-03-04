using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBuilder : MonoBehaviour {

  public float angularDrag = 0;

  private void Awake() {
    gameObject.tag = Tag.Player;

    gameObject.AddComponent<PlayerController>();
    gameObject.AddComponent<PlayerInput>();

    Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
    rigidBody.angularDrag = angularDrag;

    SphereCollider collider = gameObject.AddComponent<SphereCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);
  }

}
