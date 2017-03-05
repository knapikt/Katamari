using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState {

  private float jumpVelocity = 100.0f;

  public PlayerJumpingState(PlayerController controller) : base(controller) {
    stateName = "Jumping";
  }

  public override void Enter() {
    Vector3 v = controller.rigidBody.velocity;
    v.y = jumpVelocity;
    controller.rigidBody.velocity = v;
  }

  public override void OnGroundCollision(Collision collision, GroundController groundController) {
    controller.State = controller.GroundState;
  }

}
