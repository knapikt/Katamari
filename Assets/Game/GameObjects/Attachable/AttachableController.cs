using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AttachableController : StateControlledMonoBehavior<AttachableState, AttachableController> {
  
  public AttachableAttachedState AttachedState { get; private set; }
  public AttachableFreeState     FreeState     { get; private set; }

  public Rigidbody rigidBody { get; private set; }

  public GameObject DefaultParentGameObject { get; private set; }

  private float lastDetachTime = 0;
  private float attachCooldown = 2.0f;

  private void Awake() {
    gameObject.tag = Tag.Attachable;

    // Locate a parent object for when the Attachable is not attached
    DefaultParentGameObject = GameObject.FindGameObjectWithTag(Tag.Interactive);
    Assert.IsNotNull(DefaultParentGameObject, "Failed to locate a gameobject with tag interactive");

    // Attach a collider, set the material to something sticky
    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);

    // Attach a rigidbody
    rigidBody = gameObject.AddComponent<Rigidbody>();
  }

  private void Start() {
    // Initialize states
    AttachedState = new AttachableAttachedState(this);
    FreeState     = new AttachableFreeState(this);

    // Initialize data
    State = FreeState;
  }

  public void Attach(PlayerController playerController) {
    // Do not allow quickly occuring attachments
    if (lastDetachTime + attachCooldown < Time.time) { 
      playerController.Attach(this);
      State.Attach(playerController);
    }
  }

  public void Detach(PlayerController playerController) {
    State.Detach(playerController);
    lastDetachTime = Time.time;
  }

  public float Mass { 
    get {
      Vector3 scale = transform.localScale;
      return scale.x * scale.y * scale.z;
    }
  }

}
