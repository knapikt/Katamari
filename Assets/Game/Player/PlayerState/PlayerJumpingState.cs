using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState {

  private float jumpForce = 750.0f;

  public PlayerJumpingState(PlayerController controller) : base(controller) {
    stateName = "Jumping";
  }

  public override void Enter() {
    this.controller.rigidBody.AddForce(jumpForce * Vector3.up);
  }

  public override void OnCollision(Collision collision) {
    if (collision.gameObject.CompareTag(Tag.Ground)) {
      this.controller.State = this.controller.GroundState;
    }
  }

}
