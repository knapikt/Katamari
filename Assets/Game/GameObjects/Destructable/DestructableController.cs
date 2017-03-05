using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableController : MonoBehaviour {

  // Prefab parameters
  public float damage          = 0; // Damage applied to the player
  public float explosionForce  = 0; // Force applied to the player on collision
  public float explosionRadius = 0; // Force explosion radius

  private void Awake() {
    gameObject.tag = Tag.Destructable;

    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);

    gameObject.AddComponent<Rigidbody>();
  }

  public void Collide(Collision collision, PlayerController playerController) {
    ContactPoint contact = collision.contacts[0];

    playerController.Health -= damage;
    playerController.rigidBody.AddExplosionForce(explosionForce, contact.point, explosionRadius, 3.0f, ForceMode.VelocityChange);
    ObjectPoolController.Instance.Destroy(gameObject);
  }
}
