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
  public PlayerIdleState    IdleState    { get; private set; }

  // Private variables
  private float _health;
  private float explodeDetachVelocity = 50;

  // Settings
  public float PreferredMaxSpeed { get; private set; }

  // Delegates
  public delegate void FloatFloat(float v1, float v2);
  public delegate void Int(int number);

  // Events that are called on PlayerController state changes
  public event Int        OnAttachedCountChanged = delegate {};
  public event FloatFloat OnMassChanged          = delegate {};
  public event FloatFloat OnHealthPercentChanged = delegate {};

  private void Awake() {
    gameObject.tag = Tag.Player;

    // Hookup to Keyboard input
    gameObject.AddComponent<PlayerInput>();

    // Adding for simple physics
    rigidBody = gameObject.AddComponent<Rigidbody>();
    rigidBody.angularDrag        = 10;
    rigidBody.maxAngularVelocity = 10;

    // Adding collider with high friction
    SphereCollider collider = gameObject.AddComponent<SphereCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(ResourceConstant.Ground);
  }

  private void Start() {
    // Instantiate states
    GroundState  = new PlayerGroundState(this);
    JumpingState = new PlayerJumpingState(this);
    IdleState    = new PlayerIdleState(this);

    // Initialize settings
    State             = GroundState;
    PreferredMaxSpeed = 25.0f;     // Make sure we don't move too fast
    Health            = HealthMax; // Full health
    Mass              = 1;
  }

  protected override void FixedUpdate() {
    base.FixedUpdate();

    // Cap the speed of the object
    if (Speed > PreferredMaxSpeed) {
      Speed = PreferredMaxSpeed;
    }
  }

  // Feed collision events to the PlayerState to handle
  private void OnCollisionEnter(Collision collision) {
    switch(collision.gameObject.tag) {
    case Tag.Ground:
      var groundController = collision.gameObject.GetComponent<GroundController>();
      Assert.IsNotNull(groundController, "Failed to locate a ground controller");
      State.OnGroundCollision(collision, groundController);
      break;
    case Tag.Attachable:
      var attachableController = collision.gameObject.GetComponent<AttachableController>();
      Assert.IsNotNull(attachableController, "Failed to locate an attachable controller");
      State.OnAttachableCollision(collision, attachableController);
      break;
    case Tag.Destructable:
      var destructableController = collision.gameObject.GetComponent<DestructableController>();
      Assert.IsNotNull(destructableController, "Failed to locate an destructable controller");
      State.OnDestructableCollision(collision, destructableController);
      break;
    default:
      break;
    }
  }

  public void Move(Vector3 direction) {
    State.Move(direction);
  }

  public void Jump() {
    State.Jump();
  }

  // Remove a fraction of Attachables. Add a force so they are blown away
  public void ExplodeAttached(float fractionToExplode = 0.5f) {
    int initialAttachCount = AttachCount;
    int toRemoveCount = (int)(fractionToExplode * initialAttachCount);

    // Copy attachables to an array. This avoids errors when looping over a list that is changing in size
    AttachableController[] attachableList = new AttachableController[initialAttachCount]; 
    attached.CopyTo(attachableList);

    // Remove objects from the Player
    foreach (AttachableController attachableController in attachableList) {
      if (toRemoveCount <= 0) { 
        break;
      }

      toRemoveCount--;

      // Detach the object from the Player and explode the object
      attachableController.Detach(this);
      Detach(attachableController);
      attachableController.rigidBody.AddExplosionForce(explodeDetachVelocity, attachableController.transform.localPosition, 3f, 3f, ForceMode.VelocityChange);
    }
  }

  public void Detach(AttachableController attachableController) {
    if (attached.Contains(attachableController)) {
      attached.Remove(attachableController);
      Mass -= attachableController.Mass;
      OnAttachedCountChanged(AttachCount);
    }
  }
    
  public void Attach(AttachableController attachableController) {
    if (!attached.Contains(attachableController)) {
      attached.Add(attachableController);
      Mass += attachableController.Mass;
      OnAttachedCountChanged(AttachCount);
    }
  }

  // Properties ----------------------------------------------------------------------
  public bool Alive { get { return Health > 0; } }

  public int AttachCount { get { return attached.Count; } }

  public float Speed { 
    get { return rigidBody.velocity.magnitude; } 
    set {
      Vector3 v = rigidBody.velocity;
      v.Normalize();
      v *= value;
      rigidBody.velocity = v;
    }
  }

  public float Health    {
    get {
      return Mathf.Clamp(_health, 0, HealthMax);
    }

    set {
      float initialHealthPercent = HealthPercent;
      _health = Mathf.Clamp(value, 0, HealthMax);
      OnHealthPercentChanged(initialHealthPercent, HealthPercent);
    }
  }

  public float HealthMax { get { return 100f; } } 

  public float HealthPercent {
    get {
      return Health / HealthMax;
    }
  }

  public float Mass {
    get { return rigidBody.mass;  }
    set {
      value = Mathf.Max(1, value);

      float initialValue = Mass;
      rigidBody.mass = value; 
      transform.localScale = Vector3.one * 2 * Mathf.Pow(Mass * 0.75f * Mathf.PI, 0.33333f);
      OnMassChanged(initialValue, Mass);
    }
  }
}
