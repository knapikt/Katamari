using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableFreeState : AttachableState {

  public AttachableFreeState(AttachableController controller) : base(controller) {
    stateName = "Free";
  }

  public override void Attach(PlayerController playerController) {
    controller.gameObject.transform.parent = playerController.gameObject.transform;
    controller.State = controller.AttachedState;
  }


  public override void Enter() {
    base.Enter();
    controller.rigidBody.isKinematic = false;
    controller.rigidBody.detectCollisions = true;
  }

}
