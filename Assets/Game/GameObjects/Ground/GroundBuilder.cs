using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuilder : MonoBehaviour {

  private void Awake() {
    gameObject.tag = Tag.Ground;

    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);

  }
}
