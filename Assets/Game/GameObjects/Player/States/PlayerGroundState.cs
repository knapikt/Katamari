using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The player may move when grounded. It can also jump

public class PlayerGroundState : PlayerState {
  
  // Settings
  private float moveMultiplier = 10.0f;

  public PlayerGroundState(PlayerController controller) : base(controller) {
    stateName = "Grounded";
  }

  public override void Move(Vector3 direction) {
   // controller.rigidBody.AddForce(moveMultiplier * direction);
    controller.rigidBody.AddTorque(moveMultiplier * direction, ForceMode.VelocityChange);

  }

  public override void Jump() {
    controller.State = controller.JumpingState;
  }
}
