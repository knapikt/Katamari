using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A class for handling objects that can be destroyed and harm the player
// For now not implementing state as this is a simple class

public class DestructableController : MonoBehaviour {

  // Prefab parameters
  public float damage          = 0; // Damage applied to the player
  public float explosionForce  = 0; // Force applied to the player on collision
  public float explosionRadius = 0; // Force explosion radius

  private GameSounds gameSounds;

  public GameObject DefaultParentGameObject { get; private set; }

  private void Awake() {
    gameObject.tag = Tag.Destructable;

    // Locate other components
    gameSounds = FindObjectOfType<GameSounds>();

    // Locate a parent object for when the Attachable is not attached
    DefaultParentGameObject = GameObject.FindGameObjectWithTag(Tag.Interactive);
    Assert.IsNotNull(DefaultParentGameObject, "Failed to locate a gameobject with tag interactive");

    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(ResourceConstant.Ground);

    gameObject.AddComponent<Rigidbody>();

    // Parent to the appropriate object
    gameObject.transform.parent = DefaultParentGameObject.transform;
  }

  public void Collide(Collision collision, PlayerController playerController) {
    ContactPoint contact = collision.contacts[0];

    playerController.Health -= damage;
    playerController.rigidBody.AddExplosionForce(explosionForce, contact.point, explosionRadius, 3.0f, ForceMode.VelocityChange);
    ObjectPoolController.Instance.PutBack(gameObject);
    SoundController.Instance.PlayRandomSound(gameSounds.detaches);
  }
}
