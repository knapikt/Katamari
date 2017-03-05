using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : StateControlledMonoBehavior<PlayerState, PlayerController> {

  private HashSet<AttachableController> attached = new HashSet<AttachableController>();

  // Components
  public Rigidbody rigidBody { get; private set; }

  // States
  public PlayerGroundState  GroundState  { get; private set; }
  public PlayerJumpingState JumpingState { get; private set; }

  // Settings
  public float PreferredMaxSpeed { get; private set; }

  // Delegates
  public delegate void FloatFloat(float v1, float v2);

  public event FloatFloat OnMassChanged = delegate {};

  private void Start() {
    // Hook up components
    rigidBody = gameObject.GetComponent<Rigidbody>();

    // Instantiate states
    GroundState  = new PlayerGroundState(this);
    JumpingState = new PlayerJumpingState(this);

    // Initialize settings
    State = GroundState;
    PreferredMaxSpeed = 25.0f;
    Mass = 1;
  }

  protected override void FixedUpdate() {
    base.FixedUpdate();

    // Cap the speed of the object
    if (Speed > PreferredMaxSpeed) {
      Speed = PreferredMaxSpeed;
    }
  }

  private void OnCollisionEnter(Collision collision) {
    switch(collision.gameObject.tag) {
    case Tag.Ground:
      GroundController groundController = collision.gameObject.GetComponent<GroundController>();
      Assert.IsNotNull(groundController, "Failed to locate a ground controller");
      State.OnGroundCollision(collision, groundController);
      break;
    case Tag.Attachable:
      AttachableController attachableController = collision.gameObject.GetComponent<AttachableController>();
      Assert.IsNotNull(attachableController, "Failed to locate an attachable controller");
      State.OnAttachableCollision(collision, attachableController);
      break;
    default:
      break;
    }
  }

  public void Move(Vector3 direction) {
    this.State.Move(direction);
  }

  public void Jump() {
    this.State.Jump();
  }
    
  public void Attach(AttachableController attachableController) {
    if (!attached.Contains(attachableController)) {
      attached.Add(attachableController);
      Mass += attachableController.Mass;
    }
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

  public float HealthPercent {
    get {
      return 1f;
    }
  }

  public float Mass {
    get { return rigidBody.mass;  }
    set {
      float initialValue = Mass;
      rigidBody.mass = value; 
      transform.localScale = Vector3.one * 2 * Mathf.Pow(Mass * 0.75f * Mathf.PI, 0.33333f);
      OnMassChanged(initialValue, Mass);
    }
  }

}
