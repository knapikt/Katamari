using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState {
  
  // Settings
  private float moveMultiplier = 100.0f;

  public PlayerGroundState(PlayerController controller) : base(controller) {
    stateName = "Grounded";
  }

  public override void Move(Vector3 direction) {
    controller.rigidBody.AddForce(moveMultiplier * direction);
  }

  public override void Jump() {
    controller.State = controller.JumpingState;
  }
}
