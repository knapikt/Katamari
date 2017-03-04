using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateControlledMonoBehavior<PlayerState, PlayerController> {

  // Components
  public Rigidbody rigidBody { get; private set; }

  // States
  public PlayerGroundState  GroundState  { get; private set; }
  public PlayerJumpingState JumpingState { get; private set; }

  public void Start() {
    // Hook up components
    rigidBody = gameObject.GetComponent<Rigidbody>();

    // Instantiate states
    GroundState  = new PlayerGroundState(this);
    JumpingState = new PlayerJumpingState(this);

    this.State = GroundState;
  }

  private void OnCollisionEnter(Collision collision) {
    this.State.OnCollision(collision);
  }

  public void Move(Vector3 direction) {
    this.State.Move(direction);
  }

  public void Jump() {
    this.State.Jump();
  }
    
}
