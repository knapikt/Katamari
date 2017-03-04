using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilder : MonoBehaviour {

  private void Awake() {
    gameObject.AddComponent<PlayerController>();
    gameObject.AddComponent<PlayerInput>();
    gameObject.AddComponent<SphereCollider>();
    gameObject.AddComponent<Rigidbody>();
  }

}
