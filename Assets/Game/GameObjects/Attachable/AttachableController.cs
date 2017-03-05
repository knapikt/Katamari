using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AttachableController : StateControlledMonoBehavior<AttachableState, AttachableController> {
  
  public AttachableAttachedState AttachedState { get; private set; }
  public AttachableFreeState     FreeState     { get; private set; }

  public Rigidbody rigidBody { get; private set; }

  private void Start() {
    // Locate components
    rigidBody = gameObject.GetComponent<Rigidbody>();
    Assert.IsNotNull(rigidBody);


    // Initialize states
    AttachedState = new AttachableAttachedState(this);
    FreeState     = new AttachableFreeState(this);

    // Initialize data
    State = FreeState;
  }

  public void Attach(PlayerController playerController) {
    State.Attach(playerController);
  }

  public float Mass { 
    get {
      Vector3 scale = transform.localScale;
      return scale.x * scale.y * scale.z;
    }
  }

}
