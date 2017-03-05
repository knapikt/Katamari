using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableAttachedState : AttachableState {

  public AttachableAttachedState(AttachableController controller) : base(controller) {
    stateName = "Attached";
  }

  public override void Detach(PlayerController playerController) {
    controller.gameObject.transform.parent = controller.DefaultParentGameObject.transform;
    controller.State = controller.FreeState;
  }

  public override void Enter() {
    base.Enter();
    controller.rigidBody.isKinematic = true;
    controller.rigidBody.detectCollisions = false;
  }
}
