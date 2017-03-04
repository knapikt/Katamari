using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateControlledMonoBehavior<PlayerState, PlayerController> {

  // Components
  public Rigidbody rigidBody { get; private set; }

  // States
  public PlayerGroundState  GroundState  { get; private set; }
  public PlayerJumpingState JumpingState { get; private set; }

  // Settings
  public float PreferredMaxSpeed { get; private set; }

  private void Start() {
    // Hook up components
    rigidBody = gameObject.GetComponent<Rigidbody>();

    // Instantiate states
    GroundState  = new PlayerGroundState(this);
    JumpingState = new PlayerJumpingState(this);

    // Initialize settings
    State = GroundState;
    PreferredMaxSpeed = 25.0f;
  }

  protected override void FixedUpdate() {
    base.FixedUpdate();

    // Cap the speed of the object
    if (Speed > PreferredMaxSpeed) {
      Speed = PreferredMaxSpeed;
    }
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
    
  // Properties ----------------------------------------------------------------------
  public float Speed { 
    get { return rigidBody.velocity.magnitude; } 
    set {
      Vector3 v = rigidBody.velocity;
      v.Normalize();
      v *= value;
      rigidBody.velocity = v;
    }
  }

}
