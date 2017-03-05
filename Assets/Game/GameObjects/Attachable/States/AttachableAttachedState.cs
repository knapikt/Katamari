using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableAttachedState : AttachableState {

  public AttachableAttachedState(AttachableController controller) : base(controller) {
    stateName = "Attached";
  }

  public override void Enter() {
    base.Enter();
    controller.rigidBody.isKinematic = true;
    controller.rigidBody.detectCollisions = false;
  }

}
